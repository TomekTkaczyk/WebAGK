using AGK.Application.Specifications;
using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AGK.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	protected readonly AgkDbContext _dbContext;
	protected readonly DbSet<TEntity> _entities;

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

	public IQueryable<TEntity> Get(ISpecification<TEntity> specification = default) {
		return SpecificationEvaluator<TEntity>.GetQuery(_entities, specification);
	}

	public IQueryable<TEntity> GetPage(ISpecification<TEntity> specification, int pageNumber, int pageSize) {
		var result = Get(specification).Skip((pageNumber - 1) * pageSize);
		if(pageSize > 0) {
			result = result.Take(pageSize);
		};

		return result.AsNoTracking();
	}

	public IQueryable<TEntity> GetPage(IQueryable<TEntity> query, int pageNumber, int pageSize) {
		var result = query.Skip((pageNumber - 1) * pageSize);
		if(pageSize > 0) {
			result = result.Take(pageSize);
		};

		return result.AsNoTracking();
	}
}
