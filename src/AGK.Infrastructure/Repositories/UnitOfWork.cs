using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using AGK.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AGK.Infrastructure.Repositories;
internal sealed class UnitOfWork(
	AgkDbContext dbContext,
	IClock clock) : IUnitOfWork
{
	private readonly AgkDbContext _dbContext = dbContext;
	private readonly IClock _clock = clock;

	public Task<int> SaveChangesAsync(BaseEntity entity, CancellationToken cancellationToken = default) {

		UpdateAuditableEntities(entity);
			
		return _dbContext.SaveChangesAsync(cancellationToken);
	}

	#region Private methods

	private void UpdateAuditableEntities(BaseEntity entity) {
		var utcNow = _clock.Current();
		IEnumerable<EntityEntry<BaseEntity>> entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
		foreach(EntityEntry<BaseEntity> entityEntry in entities) {
			entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();

			if(entityEntry.State == EntityState.Added) {
				entityEntry.Property(a => a.CreateTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.CreatedById).CurrentValue = entity?.Id;
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = entity?.Id;
				entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();
			}
			if(entityEntry.State == EntityState.Modified) {
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = entity?.Id;
				entityEntry.Property(a => a.ConcurrencyStamp).CurrentValue = Guid.NewGuid();
			}
		}
	}

	#endregion
}
