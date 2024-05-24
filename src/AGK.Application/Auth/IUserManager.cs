using AGK.Domain.Entities;

namespace AGK.Application.Auth;
public interface IUserManager
{
	IQueryable<User> Users { get; }	

	Task<User> CreateAsync(User user, CancellationToken cancellationToken);
}
