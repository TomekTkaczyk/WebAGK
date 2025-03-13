using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public class InvalidPersonalIdException(object id) : WebAgkException($"Cannot set: {id.ToString()} as personal identifier.", StatusCodes.Status400BadRequest) {
    public object PersonalId { get; } = id;
}
