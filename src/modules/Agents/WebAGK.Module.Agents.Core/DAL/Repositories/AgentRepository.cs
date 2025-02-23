using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.Core.DAL.Repositories;

internal class AgentRepository(AgentsDbContext dbContext) 
    : ActiveStatusRepository<Agent,AgentsDbContext>(dbContext), IAgentRepository {

    public async Task<Agent> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(x => x.TaxId == taxId, cancellationToken);
    
    public async Task<Agent> GetByPersonalIdAsync(string personalId, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(x => x.PersonalId == personalId, cancellationToken);

    public async Task AddAsync(Agent agent, CancellationToken cancellationToken) {
        await Entities.AddAsync(agent, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(Agent agent, CancellationToken cancellationToken) {
        Entities.Update(agent);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Agent agent, CancellationToken cancellationToken) {
        Entities.Remove(agent);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Agent> GetByIdentifierAsync(string identifier, CancellationToken cancellationToken)
    {
        var _agent = await GetByTaxIdAsync(identifier, cancellationToken);
        _agent ??= await GetByPersonalIdAsync(identifier, cancellationToken);

        return _agent;
    }
}