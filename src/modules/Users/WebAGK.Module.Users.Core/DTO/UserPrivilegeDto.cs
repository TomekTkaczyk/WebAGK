namespace WebAGK.Module.Users.Core.DTO;
public class UserPrivilegeDto
{
	public Guid Id { get; set; }
	public string Role { get; set; }
	public bool IsActive { get; set; }
	public IDictionary<string, IEnumerable<string>> Claims { get; set; }

}
