using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Specifications;

internal sealed class UserByEmailSpecification(string email) 
    : Specification<User>(user => EF.Functions.ILike(user.Email, email));