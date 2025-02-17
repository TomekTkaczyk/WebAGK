using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.Core.Exceptions;

public abstract class UserException(string message, int status) : WebAGKException(message, status) { }
