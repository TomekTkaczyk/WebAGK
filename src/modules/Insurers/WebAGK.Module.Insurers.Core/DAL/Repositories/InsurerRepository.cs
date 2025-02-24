using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class InsurerRepository(InsurersDbContext dbContext)
    : Repository<Insurer, InsurersDbContext>(dbContext), IInsurerRepository;
