using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Specifications;

internal sealed class UserByIdentifierSpecification(string identifier) 
    : Specification<User>(user => 
        EF.Functions.ILike(user.Name, identifier)
        || EF.Functions.ILike(user.Email, identifier));