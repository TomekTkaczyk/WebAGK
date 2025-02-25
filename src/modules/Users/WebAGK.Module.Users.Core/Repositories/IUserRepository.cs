using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Users.Core.Repositories;

internal interface IUserRepository : IRepository<User> {
	
	// Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
	//
	// Task<User> GetByNameAsync(string name, CancellationToken cancellationToken);
	//
	// Task<User> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken);

	// Task AddAsync(User user, CancellationToken cancellationToken);
	//
	// Task UpdateAsync(User user, CancellationToken cancellationToken);
	//
	// IQueryable<User> GetAll();
	//
	// Task DeleteAsync(User user, CancellationToken cancellationToken);
}
