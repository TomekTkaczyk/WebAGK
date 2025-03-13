using MediatR;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.UseCases.Commands.ChangePassword;
internal sealed record ChangePasswordCommand(
	Guid Id,
	string CurrentPassword,
	string Password) : IRequest
{
	public ApiError Validate() {
		var _error = new ApiError();
		if (string.IsNullOrWhiteSpace(CurrentPassword)) {
			_error.AddValidationError("CurrentPassword", "currentpassword_is_empty", "CurrentPassword is empty.");
		}
		
		if (string.IsNullOrWhiteSpace(Password)) {
			_error.AddValidationError("Password", "password_is_empty", "Password is empty.");
		}
		
		return _error;
	}
}
