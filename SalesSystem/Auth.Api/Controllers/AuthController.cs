using Auth.Api.Application.DTOs;
using Auth.Api.Application.Interfaces;
using Auth.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(
        IUserRepository userRepository,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;

        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName);

        if (user == null)
        {
            return Unauthorized("Usuario inválido");
        }

        //var user_PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password,user.PasswordHash);

        if (!isValidPassword)
        {
            return Unauthorized("Password inválido");
        }

        var result = _jwtTokenGenerator.GenerateToken(user);

        return Ok(new LoginResponseDto
                {
                    Token = result.token,
                    Expiration = result.expiration
                });
    }
}
