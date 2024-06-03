using AGK.Application.Services;
using AGK.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AGK.Infrastructure.Auth;
internal sealed class PasswordManager(IPasswordHasher<IBaseEntity> passwordHasher) : IPasswordManager
{
	private readonly IPasswordHasher<IBaseEntity> _passwordHasher = passwordHasher;

	public string HashPassword(string password) => _passwordHasher.HashPassword(default, password);

	public bool VerifyHashedPassword(string password, string securePassword) 
		=> _passwordHasher.VerifyHashedPassword(default, password, securePassword) is PasswordVerificationResult.Success;
}
