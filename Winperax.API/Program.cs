using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Winperax.Api.Context;
using Winperax.Api.Services;
using Winperax.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// 🔥 JWT Settings binding
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// 🔥 JWT Config
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

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
            ValidateAudience = false, // Audience kullanmıyoruz
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero, // Token süresi tam saatinde bitsin
        };
    });

// 🔥 Authorization
builder.Services.AddAuthorization();

// 🔥 MongoDbContext + AuthService DI Register
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication(); // JWT middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
