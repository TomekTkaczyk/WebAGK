namespace AGK.Application.Services;

public interface IPasswordManager
{
    string HashPassword(string password);

    bool VerifyHashedPassword(string password, string securePassword);
}
