using Microsoft.Extensions.Options;
using TrackIt.Domain.Entities;
using TrackIt.Domain.Enums;
using TrackIt.Infrastructure.Configurations;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Services;

/// <summary>
///     Сервис заполнения таблиц БД
/// </summary>
public class DatabaseSeeder
{
    private static readonly Random Random = new();
    private readonly IOptions<AppSettings> _appSettings;

    /// <inheritdoc cref="DatabaseSeeder" />
    public DatabaseSeeder(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings;
    }

    /// <summary>
    ///     Метод для заполнения базы тестовыми данными
    /// </summary>
    public void Seed(ApplicationDbContext context)
    {
        /*// Добавляем пользователя, если его нет в базе
        if (!context.Users.Any())
        {
            var password = _appSettings.Value.UserPassword;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "test@mail.com",
                PasswordHash = hashedPassword // Хэшированный пароль
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        // Добавляем категории, если их нет
        if (!context.Categories.Any())
        {
            var userId = context.Users.FirstOrDefault()?.Id; // Получаем id первого пользователя

            if (userId != null)
            {
                context.Categories.AddRange(
                    new CategoryEntity { Id = Guid.NewGuid(), UserId = userId.Value, Name = "Зарплата", Type = CategoryType.INCOME },
                    new CategoryEntity { Id = Guid.NewGuid(), UserId = userId.Value, Name = "Еда", Type = CategoryType.EXPENSE }
                );

                context.SaveChanges();
            }
        }

        // Добавляем транзакции, если их нет
        if (!context.Transactions.Any())
        {
            var userId = context.Users.FirstOrDefault()?.Id;

            if (userId == null)
            {
                return;
            }

            var transactionList = Enumerable.Range(1, 10).Select(i => new TransactionEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId.Value,
                CategoryId = context.Categories.FirstOrDefault()?.Id ?? Guid.NewGuid(),
                Amount = Random.Next(1000, 5000),
                Description = $"Транзакция {i}"
            }).ToList();

            context.Transactions.AddRange(transactionList);
            context.SaveChanges();
        }*/
    }
}