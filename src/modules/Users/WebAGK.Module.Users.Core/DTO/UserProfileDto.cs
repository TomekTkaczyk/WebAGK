using WebAGK.Module.Users.Core.Entities;

namespace WebAGK.Module.Users.Core.DTO;
public record UserProfileDto
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string Email { get; set; }

	public string Role { get; set; }
	
	public bool ActiveStatus { get; set; }


	public static UserProfileDto Create(User user)
	{
		return new UserProfileDto()
		{
			Id = user.Id,
			Name = user.Name,
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			Role = user.Role,
			ActiveStatus = user.ActiveStatus
		};
	}
}
