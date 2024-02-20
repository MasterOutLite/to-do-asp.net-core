using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Infrastructure.Authentication;

public static class JwtExtensions
{
    public static List<Claim> CreateClaims(this ApplicationUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Actort, JsonConvert.SerializeObject(roles)),
        };


        return claims;
    }

    public static JwtSecurityToken EncodedToken(this List<Claim> claims, IConfiguration configuration)
    {
        var apiKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]!));
        var signingCredentials = new SigningCredentials(apiKey, SecurityAlgorithms.HmacSha256);
        var tokenValidity = int.Parse(configuration["Jwt:Validity"]!);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddDays(tokenValidity),
            signingCredentials
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
        var apiKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]!));
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