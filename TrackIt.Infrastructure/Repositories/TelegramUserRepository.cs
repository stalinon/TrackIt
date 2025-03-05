using Microsoft.EntityFrameworkCore;
using TrackIt.Application.Interfaces.Repositories;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Persistence;

namespace TrackIt.Infrastructure.Repositories;

/// <inheritdoc cref="ITelegramUserRepository"/>
internal sealed class TelegramUserRepository(ApplicationDbContext context)
    : GenericRepository<TelegramUserEntity>(context), ITelegramUserRepository
{
    /// <inheritdoc />
    public override IQueryable<TelegramUserEntity> GetQuery()
    {
        return base.GetQuery().Include(u => u.User);
    }
}