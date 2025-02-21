using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.Repositories;

public interface IAgentRepository {
    Task<Agent> GetAsync(Guid id, CancellationToken cancellationToken);
   
    IQueryable<Agent> Get();
    
    Task<Agent> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken); 

    Task<Agent> GetByPersonalIdAsync(string personalId, CancellationToken cancellationToken); 

    Task AddAsync(Agent agent, CancellationToken cancellationToken);
   
    Task UpdateAsync(Agent agent, CancellationToken cancellationToken);
   
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}