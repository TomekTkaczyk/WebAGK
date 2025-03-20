using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : EntityBase where TDbContext : DbContext  {
    
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TEntity> Entities;

    protected Repository(TDbContext dbContext) {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    public virtual TEntity Add(TEntity entity) {
        Entities.Add(entity);
        return entity;
    }
    
    public virtual void Update(TEntity entity) {
        Entities.Update(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }
    
    public virtual void Delete(TEntity entity) {
        DbContext.Entry(entity).State = EntityState.Deleted;
    }

    public virtual IQueryable<TEntity> Get(ISpecification<TEntity> specification = null)
        => SpecificationEvaluator<TEntity>.GetQuery(Entities, specification);
    
    public virtual IQueryable<TEntity> GetPage(ISpecification<TEntity> specification, int pageNumber, int pageSize) {
        var _result = Get(specification).Skip((pageNumber - 1) * pageSize);
        if(pageSize > 0) {
            _result = _result.Take(pageSize);
        }

        return _result.AsNoTracking();
    }
    
    public virtual IQueryable<TEntity> GetPage(IQueryable<TEntity> query, int pageNumber, int pageSize) {
        var _result = query.Skip((pageNumber - 1) * pageSize);
        if(pageSize > 0) {
            _result = _result.Take(pageSize);
        }

        return _result.AsNoTracking();    
    }
}