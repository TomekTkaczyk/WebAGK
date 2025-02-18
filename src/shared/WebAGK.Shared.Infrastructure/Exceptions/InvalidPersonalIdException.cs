using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidPersonalIdException(object id) : WebAGKException("Cannot set: {id} as personal identifier.", StatusCodes.Status400BadRequest) {
    public object PersonalId { get; } = id;
}
