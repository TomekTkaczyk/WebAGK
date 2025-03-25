using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.Events;

internal class InsurerCreatedHandler(
    IAgentRepository agentRepository,
    IInsurerRepository insurerRepository,
    INodeRepository nodeRepository,
    IUnitOfWork unitOfWork) 
    : INotificationHandler<InsurerCreatedEvent> {
    
    public async Task Handle(InsurerCreatedEvent message, CancellationToken cancellationToken) {

        var _insurer = await insurerRepository
            .Get(new ByIdSpecification<Insurer>(message.InsurerId))
            .Include(x => x.Structure)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (_insurer is null) return;

        var _agents = await agentRepository
            .Get()
            .ToListAsync(cancellationToken);
        
        foreach (var _node in _agents.Select(agent => new Node(_insurer.Structure, agent, null))) {
            nodeRepository.Add(_node);
        }

        _insurer.Structure.RenumberingStructure();

        await unitOfWork.SaveChangesAsync(cancellationToken);    
    }
}
