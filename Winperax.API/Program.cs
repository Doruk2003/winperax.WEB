using System.Text;
// FluentValidation için gerekli using'ler
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Winperax.API.Middleware; // Yeni middleware için
using Winperax.API.Services;
using Winperax.API.Settings;
using Winperax.Application.Behaviors; // ValidationBehavior için
using Winperax.Application.Services;
using Winperax.Domain.Interfaces;
using Winperax.Infrastructure.Persistence;
using Winperax.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Minimal APIs
builder.Services.AddControllers();

// FluentValidation registration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Winperax.Application.Validators.CreateCariCommandValidator>();

// MediatR registration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// ValidationBehavior registration (FluentValidation ile MediatR entegrasyonu için)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 🔥 Bind JWT Settings (from appsettings.json)
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// JWT Config
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

// Null kontrolü ekle
if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT Key is missing in configuration.");

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            ClockSkew = TimeSpan.Zero,
        };
    });

// 🔥 Authorization
builder.Services.AddAuthorization();

// ======================================================================
// 🔥 DEPENDENCY INJECTION
// ======================================================================

// MongoDB bağlamı
builder.Services.AddSingleton<IMongoContext, MongoDbContext>();

// Generic Repository DI
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Auth Service
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Global Exception Middleware (sıra önemli!)
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
