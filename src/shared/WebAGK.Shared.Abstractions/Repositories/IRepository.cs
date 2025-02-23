using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Abstractions.Repositories;

public interface IRepository<TEntity> where TEntity : IEntityBase {
    
    TEntity Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
    
    IQueryable<TEntity> Get(ISpecification<TEntity> specification = null);
    
    IQueryable<TEntity> GetPage(ISpecification<TEntity> specification, int pageNumber, int pageSize);

    IQueryable<TEntity> GetPage(IQueryable<TEntity> query, int pageNumber, int pageSize);
}
