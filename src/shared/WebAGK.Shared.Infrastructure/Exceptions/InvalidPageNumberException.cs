using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidPageNumberException(object id) 
    : WebAgkException(
        "Page number is invalid.", 
        StatusCodes.Status400BadRequest) {
    
    public object PageNumber { get; } = id;
}
