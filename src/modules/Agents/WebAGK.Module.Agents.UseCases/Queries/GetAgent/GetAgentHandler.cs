using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgent;

internal class GetAgentHandler(IAgentRepository repository) : IRequestHandler<GetAgentQuery, AgentDto> {
    
    public async Task<AgentDto> Handle(GetAgentQuery request, CancellationToken cancellationToken) {
        var _specification = new ByIdSpecification<Agent>(request.Id);
        var _agent = await repository
            .Get(_specification)
            .SingleOrDefaultAsync(cancellationToken);
        
        return AgentDto.Create(_agent);
    }
}