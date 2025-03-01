using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUserContext" />
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    /// <inheritdoc cref="UserContext" />
    public UserContext(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public Guid UserId =>
        _unitOfWork.Users.GetQuery().Where(u => u.Email == Email).Select(u => u.Id).First();

    /// <inheritdoc />
    public string? Email => _httpContextAccessor.HttpContext?.User.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                            ?? _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

    /// <inheritdoc />
    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User.Claims
                                            .Where(c => c.Type == ClaimTypes.Role)
                                            .Select(c => c.Value)
                                        ?? new List<string>();

    /// <inheritdoc />
    public async Task<UserDto> GetOrCreateCurrentUserAsync()
    {
        if (string.IsNullOrEmpty(Email))
        {
            throw new UnauthorizedAccessException("Email not found in token");
        }

        var user = await _unitOfWork.Users.GetByEmailAsync(Email);

        if (user != null)
        {
            return MapToUserDto(user);
        }

        user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = Email!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return MapToUserDto(user);
    }
    
    private static UserDto MapToUserDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}