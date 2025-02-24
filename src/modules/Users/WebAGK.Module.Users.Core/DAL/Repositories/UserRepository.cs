using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.Core.DAL.Repositories;

internal class UserRepository(UsersDbContext dbContext)
    : Repository<User, UsersDbContext>(dbContext), IUserRepository;