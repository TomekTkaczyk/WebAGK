using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

internal class InvalidPersonalIdException(object objectToValidate) 
    : WebAGKException(
        "Invalid personal identifier specified.",
        StatusCodes.Status400BadRequest) {
    
    public object PersonalId { get; } = objectToValidate;
}