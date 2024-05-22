using AGK.Domain.Extensions;
using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;
public class User : ActiveStatusEntity
{
    private User(string username)
    {							 
		Username = username;
    }

	public Name Username { get; private set; }
	public Name Lastname { get; private set; }
	public Name Firstname {  get; private set; }
	public string NormalizedName { get; private set; }
	public Email Email { get; private set; }
	public bool EmailConfirmed { get; private set; }
	public string PasswordHash { get; private set; }
	public string SecurityStamp { get; private set; }
	public string PhoneNumber { get; private set; }
	public bool PhoneNumberConfirmed { get; private set; }
	public bool TwoFactorEnabled { get; private set; }
	public DateTime LockoutEnd { get; private set; }
	public bool LockoutEnabled { get; private set; }
	public int AccessFailedCount { get; private set; }

	public static User Create(string username) {
		return new User(username);
	}

	public void SetHashPassword(string passwordHash) {
		PasswordHash = passwordHash;
	}

	public void Normalize() {
		Firstname = Firstname?.Value.CapitalizeFirstLetter().Trim() ?? "";
		Lastname = Lastname?.Value.CapitalizeFirstLetter().Trim() ?? "";
		NormalizedName = $"{Firstname} {Lastname}"; 
	}
}
