using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Commands.UpdateAgent;

internal sealed class UpdateAgentHandler(
    IAgentRepository repository,
    IAgentUnitOfWork unitOfWork) : IRequestHandler<UpdateAgentCommand> {
    public async Task Handle(UpdateAgentCommand request, CancellationToken cancellationToken) {
        var _agent = await repository
            .Get(new ByIdSpecification<Agent>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new AgentNotFoundException(request.Id);
        
        _agent.LastName = request.LastName;
        _agent.FirstName = request.FirstName;
        _agent.SecondName = request.SecondName;
        _agent.PersonalId = request.PersonalId;
        _agent.TaxId = request.TaxId;
        _agent.ActiveStatus = request.ActiveStatus;
        _agent.IsCompany  = request.IsCompany;
        _agent.Description = request.Description;
        
        repository.Update(_agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
