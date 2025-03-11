using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class IdentifierConflictException() : WebAGKException(
    "Identifier conflict.",
    StatusCodes.Status409Conflict);