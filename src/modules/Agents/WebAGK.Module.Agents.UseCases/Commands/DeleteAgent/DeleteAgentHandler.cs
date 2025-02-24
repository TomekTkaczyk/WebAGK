using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Commands.DeleteAgent;

public class DeleteAgentHandler(
    IAgentRepository repository,
    IAgentUnitOfWork unitOfWork) : IRequestHandler<DeleteAgentCommand> {
    public async Task Handle(DeleteAgentCommand request, CancellationToken cancellationToken) {
        var _agent = await repository
            .Get(new ByIdSpecification<Agent>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new AgentNotFoundException(request.Id);
        
        repository.Delete(_agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}