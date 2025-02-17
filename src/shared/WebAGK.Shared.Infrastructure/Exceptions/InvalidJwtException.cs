using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public sealed class InvalidJwtException() : WebAGKException("Invalid JWT", StatusCodes.Status401Unauthorized) { }
