using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class Repository<TEntity, TDbContext>
    where TEntity : EntityBase where TDbContext : DbContext {
    
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TEntity> Entities;

    protected Repository(TDbContext dbContext) {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }
    
    public IQueryable<TEntity> Get(ISpecification<TEntity> specification) {
        return SpecificationEvaluator<TEntity>.GetQuery(Entities, specification);
    }

    public Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken) 
        => Entities.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public IQueryable<TEntity> GetPage(ISpecification<TEntity> specification, int pageNumber, int pageSize) {
        var result = Get(specification).Skip((pageNumber - 1) * pageSize);
        if(pageSize > 0) {
            result = result.Take(pageSize);
        };

        return result.AsNoTracking();
    }
}