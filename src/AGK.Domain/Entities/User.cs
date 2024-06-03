using AGK.Domain.Enums;
using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;
public sealed class User : ActiveStatusEntity
{
	private User() { }

	private User(string username, string email, string password) : base() {
		UserName = username;
		Email = new Email(email);
		PasswordHash = password;
	}

	public Name UserName { get; private set; }
	public Name LastName { get; private set; }
	public Name FirstName { get; private set; }
	public string NormalizedName { get; private set; }
	public Email Email { get; private set; }
	public bool EmailConfirmed { get; private set; }
	public string PasswordHash { get; private set; }
	public Guid SecurityStamp { get; private set; }
	public string PhoneNumber { get; private set; }
	public bool PhoneNumberConfirmed { get; private set; }
	public bool TwoFactorEnabled { get; private set; }
	public DateTime LockoutEnd { get; private set; }
	public bool LockoutEnabled { get; private set; }
	public int AccessFailedCount { get; private set; }
	public Role Role { get; private set; }

	public static User Create(Name username, Email email, string password) {
		return new User(username, email, password);
	}

	public void Normalize() {
		FirstName = FirstName?.Value.ToUpper().Trim() ?? "";
		LastName = LastName?.Value.ToUpper().Trim() ?? "";
		NormalizedName = $"{FirstName} {LastName}";
	}

	public void Update(
		string firstName,
		string lastName,
		string phoneNumber) {
		FirstName = firstName;
		LastName = lastName;
		PhoneNumber = phoneNumber;
	}
}
