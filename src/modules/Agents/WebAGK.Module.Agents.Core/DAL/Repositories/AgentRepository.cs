using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Repositories;

namespace WebAGK.Module.Agents.Core.DAL.Repositories;

internal class AgentRepository(AgentsDbContext dbContext) : IAgentRepository {
    public Task<Agent> GetAsync(Guid id, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
    public IQueryable<Agent> Get() {
        return dbContext.Set<Agent>().AsQueryable();
    }
    public Task AddAsync(Agent agent, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
    public Task UpdateAsync(Agent agent, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}