using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Abstractions.Repositories;

public interface IActiveStatusRepository<TEntity> : IRepository<TEntity> where TEntity : IActiveStatusEntity {
    
    void SetActive(IActiveStatusEntity entity, bool activeStatus);
}