using AGK.Application.Specifications;
using AGK.Domain.Entities;

namespace AGK.Application.Features.UserManagment.Specifications;
internal sealed class UserSearchSpecification(bool? active = null, string searchString = null) 
	: Specification<User>(
		user =>	(!active.HasValue || user.ActiveStatus.Equals(active))
				&&
				(string.IsNullOrWhiteSpace(searchString) ||
				user.Username.Value.Contains(searchString) ||
				user.Firstname.Value.Contains(searchString) ||
				user.Lastname.Value.Contains(searchString))) 
{
}
