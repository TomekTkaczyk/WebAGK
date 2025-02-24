using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.Core.DAL.Repositories;

internal class AgentUnitOfWork(AgentsDbContext dbContext, IClock clock, IContext context) 
    : UnitOfWork<AgentsDbContext>(dbContext, clock, context), IAgentUnitOfWork;