using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

internal class InvalidIdentifierException() : WebAgkException(
        "Invalid identifier specified.",
        StatusCodes.Status400BadRequest) {
}