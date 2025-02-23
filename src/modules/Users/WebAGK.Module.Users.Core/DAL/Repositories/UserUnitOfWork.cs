using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.Core.DAL.Repositories;

public class UserUnitOfWork(UsersDbContext dbContext, IClock clock, IContext context) 
    : UnitOfWork<UsersDbContext>(dbContext, clock, context), IUserUnitOfWork { }