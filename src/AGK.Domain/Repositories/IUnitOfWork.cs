using AGK.Domain.Entities;

namespace AGK.Domain.Repositories;

public interface IUnitOfWork
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
