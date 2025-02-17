using Microsoft.AspNetCore.Http;

namespace WebAGK.Module.Users.Core.Exceptions;

internal class UserEmailConfirmException() : UserException("Invalid email confirm token.", StatusCodes.Status400BadRequest) { }
