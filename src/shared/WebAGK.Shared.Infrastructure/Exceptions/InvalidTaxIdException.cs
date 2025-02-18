using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidTaxIdException(object id) 
    : WebAGKException(
        "Cannot set: {id} as tax identifier.", 
        StatusCodes.Status400BadRequest) {
    
    public object TaxId { get; } = id;
}
