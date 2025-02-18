using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.Repositories;

public interface IAgentRepository {
    Task<Agent> GetAsync(Guid id, CancellationToken cancellationToken);
   
    IQueryable<Agent> Get();

    Task AddAsync(Agent agent, CancellationToken cancellationToken);
   
    Task UpdateAsync(Agent agent, CancellationToken cancellationToken);
   
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}