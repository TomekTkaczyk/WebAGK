using WebAGK.Shared.Infrastructure.Entities;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Users.Core.Entities;

public class User : EntityBase
{
	public Email Email { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public string Role { get; set; }
	public bool IsActive { get; set; }
	public IDictionary<string, IEnumerable<string>> Permissions { get; set; }
	public bool EmailConfirm { get; set; }
	public string EmailToConfirm { get; set; }
	public string EmailConfirmToken { get; set; }
	public DateTime EmailConfirmExpires { get; set; }
	public string RefreshToken { get; set; }
	public DateTime RefreshExpires { get; set; }


	public static User Create(
		string name,
		string email,
		string password,
		string emailToConfirm,
		DateTime createdAt) {
		
		return new User() {
			Id = Guid.NewGuid(),
			Name = name,
			Email = email,
			Password = password,
			Role = "User",
			Permissions = new Dictionary<string, IEnumerable<string>>(),
			EmailToConfirm = emailToConfirm,
			EmailConfirm = false,
			IsActive = true,
			CreatedAt = createdAt,
		};
	}
	
	public IEnumerable<string> GetPermissions()
	{
		List<string> result = [];
		if(Permissions is not null) {
			result = Permissions.Aggregate(
				result, 
				(current, permission) 
					=> [.. current, .. permission.Value.Select(x => $"{permission.Key}.{x}")]);
		}

		return result;
	}
}
