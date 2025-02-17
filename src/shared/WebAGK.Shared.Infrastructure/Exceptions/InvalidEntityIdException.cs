using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;

public sealed class InvalidEntityIdException(object id) : WebAGKException($"Cannot set: {id} as entity identifier.", StatusCodes.Status400BadRequest)
{
	public object Id { get; } = id;
}
