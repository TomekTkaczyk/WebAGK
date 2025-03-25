using System.Data;
using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class UnitOfWork<TDbContext>(TDbContext dbContext, IClock clock, IContext context) : IUnitOfWork
	where TDbContext : DbContext {

	public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
		
		var _utcNow = clock.CurrentDate();
		var _userId = context.Identity?.Id ?? Guid.Empty;

		var _entities = dbContext
			.ChangeTracker.Entries<EntityBase>()
			.Where(x => x.State is EntityState.Added or EntityState.Modified);
		
		foreach(var _entityEntry in _entities) {
			if(_entityEntry.State == EntityState.Added) {
				_entityEntry.Entity.SetCreateBy(_userId, _utcNow);
			}
			_entityEntry.Entity.SetModifiedBy(_userId, _utcNow);
		}

		try {
			return await dbContext.SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException _ex) {
			throw new DBConcurrencyException("Conflict record version.", _ex);
		}
	}
}