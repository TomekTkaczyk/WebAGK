using AGK.Domain.Entities;

namespace AGK.Domain.Repositories;
public interface IRepository<TEntity> where TEntity : IBaseEntity
{
	TEntity Add(TEntity entity);

	void Update(TEntity entity);

	void Delete(TEntity entity);

	IQueryable<TEntity> Get(ISpecification<TEntity> specification = default);

	Task<List<TEntity>> GetPageAsync(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

	Task<List<TEntity>> GetPageAsync(IQueryable<TEntity> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

	Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);

}
