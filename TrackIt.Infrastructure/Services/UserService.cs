using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;
using TrackIt.Domain.Interfaces;

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

    /// <inheritdoc />
    public async Task<UserDto> CreateUserAsync(string email)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        var newUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        return MapToUserDto(newUser)!;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetPaginatedUsersAsync(int pageIndex, int pageSize)
    {
        return (await _unitOfWork.Users.GetPaginatedUsersAsync(pageIndex, pageSize)).Select(MapToUserDto).ToArray()!;
    }
    
    private static UserDto? MapToUserDto(UserEntity? user)
    {
        return user == null ? null : new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}