using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Infrastructure.CQRS;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgents;

internal class GetAgentsHandler(IAgentRepository repository) : IRequestHandler<GetAgentsQuery, Page<AgentDto>> {
    public async Task<Page<AgentDto>> Handle(GetAgentsQuery request, CancellationToken cancellationToken) {
        var total = await repository.Get().CountAsync(cancellationToken);
        var query = repository.Get()
            .Where(x => request.IsActive == null || x.IsActive == request.IsActive)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ThenBy(x => x.SecondName)
            .Skip(request.PageSize * (request.PageNumber - 1));
        if (request.PageSize > 0) {
            query = query.Take(request.PageSize);
        }
        var collection = await query
            .Select(x => AgentDto.Create(x))
            .ToListAsync(cancellationToken);  
        
        return Page<AgentDto>.Create(request.PageNumber, request.PageSize, total, collection);
    }
}
