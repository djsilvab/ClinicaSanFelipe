using Auth.Api.Infrastructure.Persistence;
using Auth.Api.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Auth.Api.Repositories;
using Auth.Api.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models
        .OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type =
                Microsoft.OpenApi.Models
                .SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In =
                Microsoft.OpenApi.Models
                .ParameterLocation.Header,

            Description =
                "Ingrese el token JWT"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models
        .OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models
                .OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models
                        .OpenApiReference
                        {
                            Type =
                            Microsoft.OpenApi.Models
                            .ReferenceType
                            .SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});

builder.Services.AddDbContext<AuthDbContext>(
    options => 
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));

builder.Services
.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration[
                    "Jwt:Issuer"],

            ValidAudience =
                builder.Configuration[
                    "Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration[
                            "Jwt:Key"]!))
        };
});

builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddScoped<JwtTokenGenerator>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins?? new[] { "http://localhost:4200" } ).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
