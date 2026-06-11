using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LinkQ.Api.Model;

namespace LinkQ.Api.Infrastructure.Authentication;

public class JwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public LoginResponse GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Member_ID),
            new Claim(ClaimTypes.Name, user.Member_Name),
            new Claim(ClaimTypes.Role, user.Is_Admin ? "Admin" : "User")
        };

        var expiration = DateTime.UtcNow.AddMinutes(
            double.Parse(_config["JwtSettings:ExpirationInMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
