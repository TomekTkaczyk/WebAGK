using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class BadRequestException() : WebAgkException(
    "BadRequestException", 
    StatusCodes.Status400BadRequest);