using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Insurers.Core.Exceptions;

public class AgentNotFoundException()
    : WebAgkException("Agent not found.", StatusCodes.Status400BadRequest);
