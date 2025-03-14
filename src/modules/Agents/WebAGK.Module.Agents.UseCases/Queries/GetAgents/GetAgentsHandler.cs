using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Module.Agents.UseCases.Specifications;
using WebAGK.Shared.Infrastructure.CQRS;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgents;

internal class GetAgentsHandler(IAgentRepository repository) : IRequestHandler<GetAgentsQuery, Page<AgentDto>> {
    public async Task<Page<AgentDto>> Handle(GetAgentsQuery request, CancellationToken cancellationToken) {
        var _query = repository.Get(new SearchAgentSpecification(request.SearchText, request.IsActive));
        var _total = await _query.CountAsync(cancellationToken);
        var _agents = _query
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ThenBy(x => x.SecondName)
            .Skip(request.PageSize * (request.PageNumber - 1));
        if (request.PageSize > 0) {
            _agents = _agents.Take(request.PageSize);
        }
        var _collection = await _agents
            .Select(x => AgentDto.Create(x))
            .ToListAsync(cancellationToken);  
        
        return Page<AgentDto>.Create(request.PageNumber, request.PageSize, _total, _collection);
    }
}
