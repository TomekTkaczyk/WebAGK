using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

internal class InvalidRpuIdException(object objectToValidate) 
    : WebAgkException(
        "Cannot set: {id} as PUN identifier.", 
        StatusCodes.Status400BadRequest) {
    
    public object RpuId { get; } = objectToValidate;
}