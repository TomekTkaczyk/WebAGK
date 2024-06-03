using AGK.Domain.Entities;

namespace AGK.Domain.Repositories;
public interface IActiveStatusRepository<TEntity> : IRepository<TEntity> where TEntity : IBaseEntity
{
	void SetActive(IActiveStatus entity, bool activeStatus);
}
