using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public sealed class UnauthorisedException() : WebAgkException("Unauthorise.", StatusCodes.Status401Unauthorized) { }
