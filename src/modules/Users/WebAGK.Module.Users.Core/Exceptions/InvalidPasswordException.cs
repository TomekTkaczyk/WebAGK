using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;
internal class InvalidPasswordException() : WebAgkException("Invalid password.", StatusCodes.Status400BadRequest) { }

