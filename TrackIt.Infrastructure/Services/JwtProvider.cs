using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IJwtProvider" />
internal sealed class JwtProvider : IJwtProvider
{
    private readonly string _secretKey = "your-256-bit-secret"; // Замените на более безопасный ключ
    private readonly int _accessTokenExpiration = 15; // Время жизни токена (в минутах)
    private readonly int _refreshTokenExpiration = 7; // Время жизни рефреш токена (в днях)

    /// <inheritdoc />
    public string GenerateAccessToken(UserDto user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Добавляем claims для токена
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // уникальный идентификатор токена
            new Claim("userId", user.Id.ToString()) // Идентификатор пользователя
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "YourApp",
            Audience = "YourApp",
            Expires = DateTime.Now.AddMinutes(_accessTokenExpiration),
            SigningCredentials = credentials,
            Claims = claims.ToDictionary(c => c.Type, c => (object)c.Value)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <inheritdoc />
    public RefreshTokenDto GenerateRefreshToken(UserDto user)
    {
        var refreshToken = new RefreshTokenDto
        {
            RefreshToken = Guid.NewGuid().ToString(),
            ExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenExpiration)
        };

        return refreshToken;
    }

    /// <inheritdoc />
    public bool ValidateToken(string token, out Guid userId)
    {
        userId = Guid.Empty;

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "YourApp",
                ValidAudience = "YourApp",
                IssuerSigningKey = securityKey
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var _);
            userId = Guid.Parse(principal.Identity!.Name!); // Извлекаем userId из токена
            return true;
        }
        catch
        {
            return false;
        }
    }
}