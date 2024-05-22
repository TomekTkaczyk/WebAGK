using AGK.Domain.Entities;

namespace AGK.Domain.Repositories;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
	TEntity Add(TEntity entity);

	void Update(TEntity entity);

	void Delete(TEntity entity);

	IQueryable<TEntity> Get(ISpecification<TEntity> specification);

	Task<List<TEntity>> GetPageAsync(ISpecification<TEntity> specification,  int pageNumber, int pageSize, CancellationToken cancellationToken);

	Task<List<TEntity>> GetPageAsync(IQueryable<TEntity> query, int pageNumber, int pageSize, CancellationToken cancellationToken);

	Task<int> CountAsync(IQueryable<TEntity> query, CancellationToken cancellationToken);
}
