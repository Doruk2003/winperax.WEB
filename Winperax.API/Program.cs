using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Winperax.Infrastructure.Persistence;
using Winperax.Infrastructure.Repositories;
using Winperax.Domain.Interfaces;
using Winperax.API.Services;
using Winperax.API.Settings;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// 🔥 Bind JWT Settings (from appsettings.json)
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// JWT Config
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

builder.Services
    .AddAuthentication(options =>
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

            ClockSkew = TimeSpan.Zero
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

// ======================================================================

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
