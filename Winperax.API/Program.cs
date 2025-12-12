using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Logs; // OpenTelemetry için eklendi
using OpenTelemetry.Metrics; // OpenTelemetry için eklendi
using OpenTelemetry.Trace; // OpenTelemetry için eklendi
using Serilog; // Serilog için eklendi
using Winperax.API.Middleware; // Yeni middleware için
using Winperax.Application.Behaviors; // ValidationBehavior için
using Winperax.Application.Services;
using Winperax.Domain.Interfaces;
using Winperax.Infrastructure.Persistence;
using Winperax.Infrastructure.Repositories;
using Winperax.Infrastructure.Services; // PasswordHasherService, JwtTokenGeneratorService, AuthService için
using Winperax.Infrastructure.Settings; // ✅ JwtSettings için doğru namespace

// builder değişkenini tanımla
var builder = WebApplication.CreateBuilder(args); // ✅ builder değişkeni burada tanımlandı

// Serilog yapılandırması (builder'dan sonra)
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // appsettings.json'dan oku
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .CreateBootstrapLogger(); // Uygulama başlamadan loglama başlatılır

// Serilog'u DI konteynerine ekle
builder.Host.UseSerilog();

// OpenTelemetry'i ekle (Tracing ve Metrics için)
builder
    .Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation())
    .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation());

// Controllers & Minimal APIs
builder.Services.AddControllers();

// FluentValidation registration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Winperax.Application.Validators.CreateCariCommandValidator>();

// MediatR registration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// ValidationBehavior registration (FluentValidation ile MediatR entegrasyonu için)
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// FluentValidation hatalarını ApiResponse formatında dönmek için yapılandırma
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context
            .ModelState.Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        var response = Winperax.API.Responses.ApiResponse.FailureResult(
            errors,
            "Validation failed"
        );
        return new BadRequestObjectResult(response);
    };
});

// 🔥 Bind JWT Settings (from appsettings.json)
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt")); // ✅ JwtSettings için gerekli using eklendi

// JWT Config
var jwtKey = builder.Configuration["Jwt:Key"]!; // ✅ Null olmayan değer atandı
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!; // ✅ Null olmayan değer atandı

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

// Application Services DI (Infrastructure'daki uygulamalar)
builder.Services.AddScoped<IAuthService, AuthService>(); // ✅ Arayüz -> Uygulama
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>(); // ✅ Arayüz -> Uygulama
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorService>(); // ✅ Arayüz -> Uygulama

var app = builder.Build();

// Global Exception Middleware (sıra önemli!)
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
