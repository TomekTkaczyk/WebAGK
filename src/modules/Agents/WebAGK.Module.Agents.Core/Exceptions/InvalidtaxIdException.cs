using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

public class InvalidtaxIdException(object objectToValidate) 
    : WebAGKException(
        "Invalid identifier specified.",
        StatusCodes.Status400BadRequest) {
    
    public object TaxId { get; } = objectToValidate;
}