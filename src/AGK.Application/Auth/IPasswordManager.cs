namespace AGK.Application.Auth;

public interface IPasswordManager
{
	string HashPassword(string password);

	bool VerifyHashedPassword(string password, string securePassword);
}
