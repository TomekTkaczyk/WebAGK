using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;

namespace AGK.Application.Services;
public interface IUserManager
{
    IQueryable<User> Users { get; }

    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
	Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default);
	Task<int> DeleteAsync(User user, CancellationToken cancellationToken = default);

	Task<User> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<User> FindByUserNameAsync(Name userName, CancellationToken cancellationToken = default);
    Task<User> FindByIdAsync(EntityId id, CancellationToken cancellationToken = default);

    Task<bool> IsUnique(User user, CancellationToken cancellationToken = default);
}
