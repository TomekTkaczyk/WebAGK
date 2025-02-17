using Microsoft.AspNetCore.Identity;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;

namespace WebAGK.Module.Users.Core.Services;
internal class DataInitializerService(IUserRepository repository, IPasswordHasher<User> passwordHasher) : IDataInitializerService
{
	public async Task Initialize()
	{
		var user = await repository.GetByNameAsync("Admin", default);
		if(user == null) {
			await AddAdmin();
		}
	}

	private async Task AddAdmin()
	{
		var user = new User
		{
			Id = Guid.NewGuid(),
			Name = "Admin",
			Password = passwordHasher.HashPassword(default, ""),
			Role = "Admin",
			IsActive = true,
			EmailConfirm = true,
			Claims = new Dictionary<string, IEnumerable<string>>()
			{
				{ "Users", new List<string> { "UserManager" } }
			}
		};

		await repository.AddAsync(user, default);
	}
}
