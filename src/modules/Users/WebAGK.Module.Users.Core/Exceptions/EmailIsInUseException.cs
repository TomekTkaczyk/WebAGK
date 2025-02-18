using Microsoft.AspNetCore.Http;

namespace WebAGK.Module.Users.Core.Exceptions;

internal class EmailIsInUseException() : UserException("EmailMessage is already taken.", StatusCodes.Status400BadRequest) { }
