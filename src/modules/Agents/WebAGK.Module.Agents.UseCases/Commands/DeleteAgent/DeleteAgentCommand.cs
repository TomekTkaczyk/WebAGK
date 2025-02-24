using MediatR;

namespace WebAGK.Module.Agents.UseCases.Commands.DeleteAgent;

public sealed record DeleteAgentCommand(Guid Id) : IRequest;