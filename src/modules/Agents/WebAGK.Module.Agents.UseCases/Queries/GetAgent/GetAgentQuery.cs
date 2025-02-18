using MediatR;
using WebAGK.Module.Agents.Core.DTO;

namespace WebAGK.Module.Agents.UseCases.Queries.GetAgent;

internal sealed record GetAgentQuery(Guid Id) : IRequest<AgentDto> { }