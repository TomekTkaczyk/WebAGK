using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class UserNotActiveException(Guid id) : WebAgkException($"User with ID: {id} is not active.", StatusCodes.Status400BadRequest) 
{
	public Guid Id { get; } = id;
}
