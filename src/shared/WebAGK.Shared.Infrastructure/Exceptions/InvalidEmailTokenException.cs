using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;
public sealed class InvalidEmailTokenException() : WebAGKException("Invalid email token.", StatusCodes.Status401Unauthorized) { }
