using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProcBlazor.Application.Services;
public interface IJwtAuthenticationService
{
    string GenerateToken(string username, string role);
}

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IConfiguration _configuration;

    public JwtAuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = _configuration["Jwt:Key"];
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        var jwtExpirationInMinutes = _configuration["Jwt:ExpirationInMinutes"];

        if (string.IsNullOrEmpty(jwtKey)) {
            throw new Exception("Jwt:Key is missing in appsettings.json");
        }

        if (string.IsNullOrEmpty(jwtIssuer)) {
            throw new Exception("Jwt:Issuer is missing in appsettings.json");
        }

        if (string.IsNullOrEmpty(jwtAudience)) {
            throw new Exception("Jwt:Audience is missing in appsettings.json");
        }

        if (string.IsNullOrEmpty(jwtExpirationInMinutes)) {
            throw new Exception("Jwt:ExpirationInMinutes is missing in appsettings.json");
        }

        var key = Encoding.UTF8.GetBytes(jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtExpirationInMinutes)),
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}