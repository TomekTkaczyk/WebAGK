using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class ActiveStatusRepository<TEntity, TDbContext>(TDbContext dbContext)
    : Repository<TEntity, TDbContext>(dbContext), IActiveStatusRepository<TEntity>
    where TEntity : EntityBase, IActiveStatusEntity
    where TDbContext : DbContext {
    
    public void SetActive(IActiveStatusEntity entity, bool activeStatus) {

        entity.SetActiveStatus(activeStatus);
    }
}
