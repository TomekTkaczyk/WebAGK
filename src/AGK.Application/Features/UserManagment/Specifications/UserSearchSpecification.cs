using AGK.Application.Specifications;
using AGK.Domain.Entities;

namespace AGK.Application.Features.UserManagment.Specifications;
internal sealed class UserSearchSpecification(bool? activeStatus = null, string searchString = null)
	: Specification<User>(
		user => 
			(activeStatus==null
			|| user.ActiveStatus == activeStatus) 
			&& 
			(string.IsNullOrEmpty(searchString) 
			|| ((string)user.UserName).Contains(searchString)
			|| ((string)user.FirstName).Contains(searchString)
			|| ((string)user.LastName).Contains(searchString))
		) { }
