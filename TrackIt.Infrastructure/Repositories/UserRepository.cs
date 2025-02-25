using Microsoft.EntityFrameworkCore;
using TrackIt.Domain.Entities;
using TrackIt.Domain.Interfaces.Repositories;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="IUserRepository" />
public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    /// <inheritdoc cref="UserRepository" />
    public UserRepository(ApplicationDbContext context) : base(context) { }

    /// <inheritdoc />
    public async Task<UserEntity?> GetByEmailAsync(string email) => await DbSet.FirstOrDefaultAsync(u => u.Email == email);

    /// <inheritdoc />
    public async Task<IEnumerable<UserEntity>> GetPaginatedUsersAsync(int pageIndex, int pageSize) => await GetPaginatedAsync(pageIndex, pageSize, null, u => u.CreatedAt, true);
}