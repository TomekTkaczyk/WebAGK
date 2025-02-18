using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Users.Core.Entities;

public class User : EntityBase
{
	public Email Email { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Role { get; set; }
	public bool IsActive { get; set; }
	public IDictionary<string, IEnumerable<string>> Claims { get; set; }  // change name to Permissions on next migration !!!
	public bool EmailConfirm { get; set; }
	public string EmailToConfirm { get; set; }
	public string EmailConfirmToken { get; set; }
	public DateTime EmailConfirmExpires { get; set; }
	public string RefreshToken { get; set; }
	public DateTime RefreshExpires { get; set; }


	public IEnumerable<string> GetPermissions()
	{
		List<string> result = [];
		if(Claims is not null) {
			foreach(var permission in Claims) {
				result = [.. result, .. permission.Value.Select(x => $"{permission.Key}.{x}")];
			}
		}

		return result;
	}
}
