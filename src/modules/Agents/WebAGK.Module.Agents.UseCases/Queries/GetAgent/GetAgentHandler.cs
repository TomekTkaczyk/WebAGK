using MediatR;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Module.Agents.Core.Repositories;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgent;

internal class GetAgentHandler(IAgentRepository repository) : IRequestHandler<GetAgentQuery, AgentDto> {
    
    public async Task<AgentDto> Handle(GetAgentQuery request, CancellationToken cancellationToken) {
        var agent = await repository.GetAsync(request.Id, cancellationToken);
        
        return AgentDto.Create(agent);
    }
}