using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using AGK.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AGK.Infrastructure.Repositories;
internal sealed class UnitOfWork : IUnitOfWork
{
	private readonly AgkDbContext _dbContext;
	private readonly IClock _clock;

	public UnitOfWork(AgkDbContext dbContext, IClock clock) {
		_dbContext = dbContext;
		_clock = clock;
	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
		UpdateAuditableEntities();

		return _dbContext.SaveChangesAsync(cancellationToken);
	}

	#region Private methods

	private void UpdateAuditableEntities() {
		IEnumerable<EntityEntry<BaseEntity>> entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
		foreach(EntityEntry<BaseEntity> entityEntry in entities) {
			if(entityEntry.State == EntityState.Added) {
				var utcNow = _clock.Current();
				entityEntry.Property(a => a.CreateTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.CreatedById).CurrentValue = 1;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = 1;
			}
			if(entityEntry.State == EntityState.Modified) {
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = _clock.Current();
				entityEntry.Property(a => a.ModifiedById).CurrentValue = 1;
			}
		}
	}

	#endregion
}
