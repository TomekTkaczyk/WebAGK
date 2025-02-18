using MediatR;
using WebAGK.Module.Agents.Core.DTO;
using WebAGK.Shared.Infrastructure.CQRS;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgents;

internal sealed record GetAgentsQuery(string SearchText, PageNumber PageNumber, PageSize PageSize, bool? IsActive ) : IRequest<Page<AgentDto>> {
    
}