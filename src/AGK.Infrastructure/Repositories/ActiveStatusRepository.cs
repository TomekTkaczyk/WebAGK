using AGK.Application.Specifications.Common;
using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AGK.Infrastructure.Repositories;
public class ActiveStatusRepository<TEntity>
	: Repository<TEntity>, IActiveStatusRepository<TEntity> where TEntity : ActiveStatusEntity
{
	protected ActiveStatusRepository(AgkDbContext dbContext) : base(dbContext) { }

	public void SetActive(IActiveStatus entity, bool activeStatus) {

		entity.SetActiveStatus(activeStatus);
	}
}
