using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidPageSizeException(object id) 
    : WebAgkException(
        "Page size is invalid.", 
        StatusCodes.Status400BadRequest) {
    
    public object PageSize { get; } = id;
}
