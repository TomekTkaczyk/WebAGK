using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.Core.DAL.Repositories;
internal class UserRepository(UsersDbContext context) 
	: Repository<User,UsersDbContext>(context), IUserRepository
{
	public async Task<User> GetByEmailToken(string token, CancellationToken cancellationToken)
		=> await Entities.SingleOrDefaultAsync(x => x.EmailConfirmToken.Equals(token), cancellationToken);

	public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
		=> await Entities.SingleOrDefaultAsync(x => EF.Functions.ILike(x.Email, email), cancellationToken);

	public async Task<User> GetByNameAsync(string name, CancellationToken cancellationToken)
		=> await Entities.SingleOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);

	public async Task AddAsync(User user, CancellationToken cancellationToken)
	{
		await Entities.AddAsync(user, cancellationToken);
		await DbContext.SaveChangesAsync(cancellationToken);
	}
	public async Task UpdateAsync(User user, CancellationToken cancellationToken)
	{
		Entities.Update(user);
		await DbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(User user, CancellationToken cancellationToken)
	{
		Entities.Remove(user);
		await DbContext.SaveChangesAsync(cancellationToken);
	}

	public IQueryable<User> GetAll()
	{
		return Entities.OrderBy(x => x.Name).AsNoTracking();
	}

	public async Task<User> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken)
	{
		var user = await GetByNameAsync(identifier, cancellationToken);
		user ??= await GetByEmailAsync(identifier, cancellationToken);

		return user;
	}
}
