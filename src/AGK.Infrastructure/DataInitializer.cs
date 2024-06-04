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

		CreateUserIfNotExistAsync("Admin", "support@unipromax.pl", "").Wait();
		CreateUserIfNotExistAsync("Perlon", "perlon@unipromax.pl", "").Wait();
		CreateUserIfNotExistAsync("KS", "ks@unipromax.pl", "").Wait();
		CreateUserIfNotExistAsync("GS", "gs@unipromax.pl", "").Wait();
	}

	private async Task<User> CreateUserIfNotExistAsync(Name userName, Email email, string password) {

		User user;
		try {
			user = await _userManager.FindByUserNameAsync(userName);
		}
		catch {
			user = User.Create(
				userName, 
				email, 
				_passwordManager.HashPassword(password));

			await _userManager.CreateAsync(user);
		}

		return user;
	}
}
