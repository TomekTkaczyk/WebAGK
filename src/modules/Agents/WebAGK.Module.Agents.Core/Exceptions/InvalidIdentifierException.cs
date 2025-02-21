using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class InvalidIdentifierException() : WebAGKException(
        "Invalid identifier specified.",
        StatusCodes.Status400BadRequest) {
}