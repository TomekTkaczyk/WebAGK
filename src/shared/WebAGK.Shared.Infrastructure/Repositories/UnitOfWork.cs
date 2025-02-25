using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class UnitOfWork<TDbContext>(TDbContext dbContext, IClock clock, IContext context) : IUnitOfWork
	where TDbContext : DbContext {
	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		
		var _utcNow = clock.CurrentDate();
		var _userId = context.Identity?.Id ?? Guid.Empty;

		var _entities = dbContext.ChangeTracker.Entries<EntityBase>();
		foreach(var _entityEntry in _entities) {
			_entityEntry.Entity.SetConcurrencyStamp();
			if(_entityEntry.State == EntityState.Added) {
				_entityEntry.Entity.SetCreateBy(_userId, _utcNow);
			}
			_entityEntry.Entity.SetModifiedBy(_userId, _utcNow);
		}

		return await dbContext.SaveChangesAsync(cancellationToken);
	}
}