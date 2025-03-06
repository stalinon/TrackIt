using Microsoft.EntityFrameworkCore;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IUserRepository" />
internal sealed class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    /// <inheritdoc cref="UserRepository" />
    public UserRepository(ApplicationDbContext context) : base(context) { }

    /// <inheritdoc />
    public override IQueryable<UserEntity> GetQuery()
    {
        return base.GetQuery().Include(u => u.TelegramUser);
    }

    /// <inheritdoc />
    public async Task<UserEntity?> GetByEmailAsync(string email) => await GetQuery().FirstOrDefaultAsync(u => u.Email == email);

    /// <inheritdoc />
    public async Task<IEnumerable<UserEntity>> GetPaginatedUsersAsync(int pageIndex, int pageSize) => await GetPaginatedAsync(pageIndex, pageSize, null, u => u.CreatedAt, true);
}