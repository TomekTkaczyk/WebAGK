using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class InvalidCredentialsException() : WebAGKException(
	"Invalid credentials.", StatusCodes.Status400BadRequest)
{
}
