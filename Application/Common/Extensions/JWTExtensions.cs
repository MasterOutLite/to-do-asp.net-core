using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Extensions;

public static class JWTExtensions
{
    public static List<Claim> CreateClaims(this ApplicationUser user, List<ApplicationRole> roles)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, string.Join(' ', roles.Select(role => role.Name))),
        };

        return claims;
    }

    public static JwtSecurityToken EncodedToken(this List<Claim> claims, IConfiguration configuration)
    {
        var apiKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ApiKey"]!));
        var tokenValidity = int.Parse(configuration["JWT:Validity"]!);

        var token = new JwtSecurityToken(
            configuration["JWT:Issuer"],
            configuration["JWT:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(tokenValidity),
            signingCredentials: new SigningCredentials(apiKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    public static string GetToken(this JwtSecurityToken token)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public static bool ValidateJwtToken(IConfiguration configuration, string token, out ClaimsPrincipal principal)
    {
        var apiKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ApiKey"]!));
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = apiKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };

        try
        {
            SecurityToken validatedToken;
            principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return true;
        }
        catch (Exception)
        {
            principal = null;
            return false;
        }
    }
}