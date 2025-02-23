using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Specifications;
internal sealed class UserSearchSpecification(bool? isActive = null, string searchText = null)
	: Specification<User>(
		user => 
			(isActive == null || user.IsActive == isActive) 
			&& 
			(string.IsNullOrWhiteSpace(searchText) 
			|| EF.Functions.ILike(user.Name, $"%{searchText}%"))
			|| EF.Functions.ILike(user.FirstName, $"%{searchText}%")
			|| EF.Functions.ILike(user.LastName, $"%{searchText}%")
		) { }
