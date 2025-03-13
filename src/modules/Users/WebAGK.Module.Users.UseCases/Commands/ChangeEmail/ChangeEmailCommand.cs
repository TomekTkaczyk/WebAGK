using MediatR;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Users.UseCases.Commands.ChangeEmail;
internal sealed record ChangeEmailCommand(
	Guid Id,
	string Email,
	string ConfirmEmailUrl
) : IRequest {
	public ApiError Validate() {
		var _error = new ApiError();
		if (!Shared.Infrastructure.ValueObjects.Email.IsValid(Email)) {
			_error.AddValidationError("Email", "email_is_invalid", "Email is invalid");
		}

		if (string.IsNullOrWhiteSpace(ConfirmEmailUrl)) {
			_error.AddValidationError("ConfirmEmailUrl","confirmemailurl_is_invalid", "ConfirmEmailUrl is invalid");
		}
		
		return _error;
	}
}
