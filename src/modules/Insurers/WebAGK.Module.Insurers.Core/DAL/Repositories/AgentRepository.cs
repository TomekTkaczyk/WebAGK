using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

internal class AgentRepository(InsurersDbContext dbContext)
    : Repository<Agent, InsurersDbContext>(dbContext), IAgentRepository;
