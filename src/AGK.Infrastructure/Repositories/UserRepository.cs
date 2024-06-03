using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;

namespace AGK.Infrastructure.Repositories;

public class UserRepository(AgkDbContext dbContext) : ActiveStatusRepository<User>(dbContext), IUserRepository
{
}
