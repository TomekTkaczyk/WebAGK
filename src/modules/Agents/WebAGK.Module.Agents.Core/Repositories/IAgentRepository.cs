using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Agents.Core.Repositories;

public interface IAgentRepository : IActiveStatusRepository<Agent> {
    //
    // Task<Agent> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken); 
    //
    // Task<Agent> GetByPersonalIdAsync(string personalId, CancellationToken cancellationToken); 
    //
    // Task AddAsync(Agent agent, CancellationToken cancellationToken);
    //
    // Task UpdateAsync(Agent agent, CancellationToken cancellationToken);
    //
    // Task DeleteAsync(Agent agent, CancellationToken cancellationToken);
    //
    // Task<Agent> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken);
}