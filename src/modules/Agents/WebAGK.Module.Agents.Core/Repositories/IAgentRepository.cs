using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Module.Agents.Core.Repositories;

public interface IAgentRepository : IRepository<Agent> {
    Task<Agent> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken);
    Task<Agent> GetByPersonalIdAsync(string personalId, CancellationToken cancellationToken);
    Task<Agent> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken);
};