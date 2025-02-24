using TrackIt.Application.DTOs;
using TrackIt.Application.Interfaces;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Services;

/// <inheritdoc cref="IAuthService"/>
internal sealed class AuthService(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    : IAuthService
{
    /// <inheritdoc />
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Проверка на существующего пользователя
        var userRepository = unitOfWork.Repository<UserEntity>();
        var existingUser = await userRepository.FindAsync(u => u.Email == request.Email);
        
        if (existingUser != null)
        {
            throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        // Хэшируем пароль
        var hashedPassword = passwordHasher.HashPassword(request.Password);

        // Создаем нового пользователя
        var newUser = new UserEntity
        {
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        var user = await userRepository.AddAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        // Генерируем токены
        var accessToken = jwtProvider.GenerateAccessToken(new UserDto { Id = newUser.Id, Email = newUser.Email });
        var refreshToken = jwtProvider.GenerateRefreshToken(new UserDto { Id = newUser.Id, Email = newUser.Email });

        // Сохраняем рефреш-токен
        var refreshTokenRepository = unitOfWork.Repository<RefreshTokenEntity>();
        await refreshTokenRepository.AddAsync(new RefreshTokenEntity
        {
            Token = refreshToken.RefreshToken,
            ExpiryDate = refreshToken.ExpiryTime,
            UserId = user.Id
        });
        await unitOfWork.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.RefreshToken
        };
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        // Проверяем наличие пользователя в базе
        var userRepository = unitOfWork.Repository<UserEntity>();
        var user = (await userRepository.FindAsync(u => u.Email == request.Email)).FirstOrDefault();

        if (user == null || !passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            throw new UnauthorizedAccessException("Неверный email или пароль.");
        }

        // Генерация токенов
        var accessToken = jwtProvider.GenerateAccessToken(new UserDto { Id = user.Id, Email = user.Email });
        var refreshToken = jwtProvider.GenerateRefreshToken(new UserDto { Id = user.Id, Email = user.Email });

        // Сохраняем рефреш-токен
        var refreshTokenRepository = unitOfWork.Repository<RefreshTokenEntity>();
        await refreshTokenRepository.AddAsync(new RefreshTokenEntity
        {
            Token = refreshToken.RefreshToken,
            ExpiryDate = refreshToken.ExpiryTime,
            UserId = user.Id
        });
        await unitOfWork.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.RefreshToken
        };
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var refreshTokenRepository = unitOfWork.Repository<RefreshTokenEntity>();
        var existingToken = (await refreshTokenRepository.FindAsync(t => t.Token == request.RefreshToken)).FirstOrDefault();

        if (existingToken == null || existingToken.ExpiryDate < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Неверный или просроченный рефреш-токен.");
        }

        // Генерация новых токенов
        var accessToken = jwtProvider.GenerateAccessToken(new UserDto { Id = existingToken.UserId, Email = existingToken.User.Email });
        var refreshToken = jwtProvider.GenerateRefreshToken(new UserDto { Id = existingToken.UserId, Email = existingToken.User.Email });

        // Удаляем старый рефреш-токен
        await refreshTokenRepository.DeleteAsync(existingToken.Id);
        await unitOfWork.SaveChangesAsync();

        // Сохраняем новый рефреш-токен
        await refreshTokenRepository.AddAsync(new RefreshTokenEntity
        {
            Token = refreshToken.RefreshToken,
            ExpiryDate = refreshToken.ExpiryTime,
            UserId = existingToken.UserId
        });
        await unitOfWork.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.RefreshToken
        };
    }
}
