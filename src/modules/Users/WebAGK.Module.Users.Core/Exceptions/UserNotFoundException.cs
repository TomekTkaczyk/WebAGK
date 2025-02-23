using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class UserNotFoundException(Guid id) : WebAGKException($"User with ID: {id} is not exist.", StatusCodes.Status400BadRequest) 
{
	public Guid Id { get; } = id;
}
