using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using AGK.Domain.Services;
using AGK.Domain.ValueObjects;
using AGK.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AGK.Infrastructure.Repositories;
internal sealed class UnitOfWork(
	AgkDbContext dbContext,
	CurrentUserService currentUserService,
	IClock clock) : IUnitOfWork
{
	private readonly AgkDbContext _dbContext = dbContext;
	private readonly CurrentUserService _userService = currentUserService;
	private readonly IClock _clock = clock;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

		var utcNow = _clock.Current();
		var userId = _userService.GetCurrentUser().Id;
		IEnumerable<EntityEntry<BaseEntity>> entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
		foreach(EntityEntry<BaseEntity> entityEntry in entities) {
			entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();

			if(entityEntry.State == EntityState.Added) {
				entityEntry.Property(a => a.CreateTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.CreatedById).CurrentValue = userId;
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = userId;
				entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();
			}
			if(entityEntry.State == EntityState.Modified) {
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = userId;
				entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();
			}
		}

		return _dbContext.SaveChangesAsync(cancellationToken);
	}
}
