using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ChessWebApp.Api.Domain;
using Microsoft.IdentityModel.Tokens;

namespace ChessWebApp.Api.Authentication;

public sealed class LoginAuthentication
{
    private readonly IConfiguration _configuration;

    public LoginAuthentication(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string SaltHashSha256(string str, string salt)
    {
        SHA256 hash = SHA256.Create();
        byte[] strBytes = Encoding.UTF8.GetBytes($"{str}{salt}");
        byte[] hashedStrBytes = hash.ComputeHash(strBytes);
        return Convert.ToHexString(hashedStrBytes);
    }

    public string GenerateJwt(User user)
    {
        string issuer = _configuration.GetValue<string>("Jwt:Issuers:Https");
        string audience = _configuration.GetValue<string>("Url:UiHttps");
        byte[] keyBytes = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
        SymmetricSecurityKey securityKey = new(keyBytes);
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityTokenHandler jwtTokenHandler = new();

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username.Value),
                new(JwtRegisteredClaimNames.Email, user.Email.Value),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.Now.AddMonths(6),
            Audience = audience,
            Issuer = issuer,
            SigningCredentials = credentials
        };

        SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }
}