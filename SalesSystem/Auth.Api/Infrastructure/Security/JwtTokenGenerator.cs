using Auth.Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructure.Security;

public class JwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string token, DateTime expiration) GenerateToken(User user) {
        var expiration = DateTime.UtcNow.AddMinutes(30);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                user.UserName),

            new Claim(
                ClaimTypes.Role,
                user.Role),

            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString())
        };

        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires: expiration,

                signingCredentials:
                    credentials);

        var tokenString =
            new JwtSecurityTokenHandler()
            .WriteToken(token);

        return (tokenString,
            expiration);
    }
}
