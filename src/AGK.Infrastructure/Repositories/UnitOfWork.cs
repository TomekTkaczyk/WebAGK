using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using AGK.Domain.Services;
using AGK.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AGK.Infrastructure.Repositories;
internal sealed class UnitOfWork(
	AgkDbContext dbContext,
	AuthorizedUserService userService,
	IClock clock) : IUnitOfWork
{
	private readonly AgkDbContext _dbContext = dbContext;
	private readonly AuthorizedUserService _userService = userService;
	private readonly IClock _clock = clock;

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

		var utcNow = _clock.Current();
		var userId = _userService.AuthorizedUser.Id;

		IEnumerable<EntityEntry<BaseEntity>> entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
		foreach(EntityEntry<BaseEntity> entityEntry in entities) {
			entityEntry.Entity.SetConcurrencyStamp();
			if(entityEntry.State == EntityState.Added) {
				entityEntry.Entity.SetCreateOwner(userId, utcNow);
			}
			entityEntry.Entity.SetModifyOwner(userId, utcNow);
		}

		return await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
