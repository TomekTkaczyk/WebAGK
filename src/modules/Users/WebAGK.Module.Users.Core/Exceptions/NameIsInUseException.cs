using Microsoft.AspNetCore.Http;

namespace WebAGK.Module.Users.Core.Exceptions;

internal class NameIsInUseException() : UserException("Name is already taken.", StatusCodes.Status400BadRequest) { }
