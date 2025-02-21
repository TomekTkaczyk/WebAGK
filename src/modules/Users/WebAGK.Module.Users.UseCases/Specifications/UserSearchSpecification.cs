using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Specifications;
internal sealed class UserSearchSpecification(bool? isActive = null, string searchString = null)
	: Specification<User>(
		user => 
			(isActive == null || user.IsActive == isActive) 
			&& 
			(string.IsNullOrEmpty(searchString) 
			|| ((string)user.Name).Contains(searchString)
			|| ((string)user.FirstName).Contains(searchString)
			|| ((string)user.LastName).Contains(searchString))
		) { }
