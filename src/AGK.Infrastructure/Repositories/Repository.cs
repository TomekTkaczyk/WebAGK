using AGK.Application.Specifications;
using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AGK.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	private readonly AgkDbContext _dbContext;
	private readonly DbSet<TEntity> _entities;

	protected Repository(AgkDbContext dbContext)
    {
		_dbContext = dbContext;
		_entities = _dbContext.Set<TEntity>();
	}

	public TEntity Add(TEntity entity) {
		_entities.Add(entity);
		return entity;
	}

	public void Delete(TEntity entity) {
		_dbContext.Entry(entity).State = EntityState.Deleted;
	}


	public void Update(TEntity entity) {
		_entities.Update(entity);
		_dbContext.Entry(entity).State = EntityState.Modified;
	}

	public IQueryable<TEntity> Get(ISpecification<TEntity> specification = null) => 
		SpecificationEvaluator<TEntity>.GetQuery(_entities,specification);

	public Task<List<TEntity>> GetPageAsync(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default) {
		var collection = Get(specification);
		var page = collection.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync(cancellationToken);

		return page;
	}

	public Task<List<TEntity>> GetPageAsync(IQueryable<TEntity> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default) {
		return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
	}

	public Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationTokem = default) {
		return query.CountAsync(cancellationTokem);
	}
}
