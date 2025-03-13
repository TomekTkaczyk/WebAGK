using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Insurers.Core.Exceptions;

internal class InvalidInsurerNameException() : WebAgkException(
    "Invalid name specified.",
    StatusCodes.Status400BadRequest) {
}