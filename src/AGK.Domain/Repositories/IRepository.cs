using AGK.Domain.Entities;

namespace AGK.Domain.Repositories;
public interface IRepository<TEntity> where TEntity : IBaseEntity
{
	TEntity Add(TEntity entity);

	void Update(TEntity entity);

	void Delete(TEntity entity);

	IQueryable<TEntity> Get(ISpecification<TEntity> specification = default);

	IQueryable<TEntity> GetPage(ISpecification<TEntity> specification, int pageNumber, int pageSize);

	IQueryable<TEntity> GetPage(IQueryable<TEntity> query, int pageNumber, int pageSize);
}
