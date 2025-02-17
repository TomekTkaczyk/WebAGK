using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;

namespace WebAGK.Module.Users.Core.DAL.Repositories;
internal class UserRepository(UsersDbContext context) : IUserRepository
{
	private readonly UsersDbContext _context = context;
	private readonly DbSet<User> _users = context.Set<User>();


	public async Task<User> GetByEmailToken(string token, CancellationToken cancellationToken = default)
		=> await _users.SingleOrDefaultAsync(x => x.EmailConfirmToken.Equals(token), cancellationToken);

	public Task<User> GetAsync(Guid id, CancellationToken cancellationToken = default)
		=> _users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

	public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
		=> await _users.SingleOrDefaultAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken);

	public async Task<User> GetByNameAsync(string name, CancellationToken cancellationToken = default)
		=> await _users.SingleOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);

	public async Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		await _users.AddAsync(user);
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
	{
		_users.Update(user);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
	{
		_users.Remove(user);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public IQueryable<User> GetAll()
	{
		return _users.AsNoTracking();
	}

	public async Task<User> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken)
	{
		var user = await GetByNameAsync(identifier, cancellationToken);
		user ??= await GetByEmailAsync(identifier, cancellationToken);

		return user;
	}
}
