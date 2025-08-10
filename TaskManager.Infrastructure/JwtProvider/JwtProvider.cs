using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Interfaces.JwtProvider;
using TaskManager.Domain.Models;
using TaskManager.Infrastructure.Data.Configuration;

namespace TaskManager.Infrastructure.JwtProvider;

public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string CreateToken(User user)
    {
        Claim[] claims = [new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.TokenSecret)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpireHours)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}