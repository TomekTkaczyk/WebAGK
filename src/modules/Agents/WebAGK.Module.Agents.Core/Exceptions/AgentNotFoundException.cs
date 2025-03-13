using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;
internal class AgentNotFoundException(Guid id)
    : WebAgkException($"Agent with ID: {id} is not exist.", StatusCodes.Status400BadRequest) 
{
    public Guid Id { get; } = id;
}
