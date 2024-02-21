using Application.Abstractions.Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Infrastructure.Options;

namespace Infrastructure.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public string CreateToken(ApplicationUser user, IEnumerable<string> role)
    {
        var claims = new Claim[]
        {
            new(JwtClaims.Id, user.Id.ToString()),
            new(JwtClaims.Username, user.UserName!),
            new(JwtClaims.Email, user.Email!),
            new(JwtClaims.Role, JsonConvert.SerializeObject(role)),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(_jwtOptions.Validity),
            signingCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}