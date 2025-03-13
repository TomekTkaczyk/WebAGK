using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.Core.Exceptions;

internal class InvalidTaxIdException(object objectToValidate) 
    : WebAgkException(
        "Invalid identifier specified.",
        StatusCodes.Status400BadRequest) {
    
    public object TaxId { get; } = objectToValidate;
}