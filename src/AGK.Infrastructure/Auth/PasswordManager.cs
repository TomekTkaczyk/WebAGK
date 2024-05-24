using AGK.Application.Auth;
using AGK.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AGK.Infrastructure.Auth;
internal sealed class PasswordManager : IPasswordManager
{
	private readonly IPasswordHasher<IBaseEntity> _passwordHasher;

	public PasswordManager(IPasswordHasher<IBaseEntity> passwordHasher)
    {
		_passwordHasher = passwordHasher;
	}
    public string HashPassword(string password) => _passwordHasher.HashPassword(default, password);

	public bool VerifyHashedPassword(string password, string securePassword) 
		=> _passwordHasher.VerifyHashedPassword(default, password, securePassword) is PasswordVerificationResult.Success;
}
