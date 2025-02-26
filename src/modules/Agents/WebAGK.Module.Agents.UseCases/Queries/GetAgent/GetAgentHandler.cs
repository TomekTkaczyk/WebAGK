using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgent;

internal sealed class GetAgentHandler(IAgentRepository repository) : IRequestHandler<GetAgentQuery, AgentDto> {
    
    public async Task<AgentDto> Handle(GetAgentQuery request, CancellationToken cancellationToken) {
        var _agent = await repository
            .Get(new ByIdSpecification<Agent>(request.Id))
            .SingleOrDefaultAsync(cancellationToken);
        
        return AgentDto.Create(_agent);
    }
}