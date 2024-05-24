using AGK.Domain.Enums;
using AGK.Domain.ValueObjects;

namespace AGK.Domain.Entities;
public class User : ActiveStatusEntity
{
	protected User() { }

    private User(string username, Email email, string password) : base()
    {							 
		Username = username;
		Email = email;
		PasswordHash = password;
    }

	public Name Username { get; private set; }
	public Name Lastname { get; private set; }
	public Name Firstname {  get; private set; }
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
	public ICollection<string> Claims { get; private set; }

	public static User Create(string username, Email email, string password) {
		return new User(username, email, password);
	}

	public void Normalize() {
		Firstname = Firstname?.Value.ToUpper().Trim() ?? "";
		Lastname = Lastname?.Value.ToUpper().Trim() ?? "";
		NormalizedName = $"{Firstname} {Lastname}"; 
	}
}
