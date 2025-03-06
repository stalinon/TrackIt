using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IUserService" />
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    /// <inheritdoc cref="UserService" />
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        return MapToUserDto(await _unitOfWork.Users.GetByEmailAsync(email));
    }
    
    private static UserDto? MapToUserDto(UserEntity? user)
    {
        return user == null ? null : new UserDto
        {
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            LinkedTelegram = user.TelegramUser != null
        };
    }
}