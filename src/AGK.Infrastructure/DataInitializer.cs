using AGK.Application.Services;
using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;

namespace AGK.Infrastructure;
public class DataInitializer(
	IUserManager userManager,
	IPasswordManager passwordManager)
{
	private readonly IUserManager _userManager = userManager;
	private readonly IPasswordManager _passwordManager = passwordManager;

	public void UserInit() {

		CreateUserIfNotExistAsync("Admin", "support@unipromax.pl", "").GetAwaiter().GetResult();
		CreateUserIfNotExistAsync("Perlon", "perlon@unipromax.pl", "").GetAwaiter().GetResult();
		CreateUserIfNotExistAsync("KS", "ks@unipromax.pl", "").GetAwaiter().GetResult();
		CreateUserIfNotExistAsync("GS", "gs@unipromax.pl", "").GetAwaiter().GetResult();
	}

	private async Task CreateUserIfNotExistAsync(UserName userName, Email email, string password) {

		var user = User.Create(userName, email, _passwordManager.HashPassword(password));
		var isUnique = await _userManager.IsUniqueAsync(user);
		if(isUnique) {
			await _userManager.AddAsync(user);
		}
	}
}
