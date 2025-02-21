using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class InvalidRpuIdException(object objectToValidate) 
    : WebAGKException(
        "Cannot set: {id} as PUN identifier.", 
        StatusCodes.Status400BadRequest) {
    
    public object RpuId { get; } = objectToValidate;
}