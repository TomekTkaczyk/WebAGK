using AGK.Application.Auth;
using AGK.Application.Reponse;
using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

internal sealed class SignUpHandler : IRequestHandler<SignUp, ApiResponse<EntityId>>
{
	private readonly IPasswordManager _passwordManager;

	public SignUpHandler(IPasswordManager passwordManager)
    {
		_passwordManager = passwordManager;
	}

    public Task<ApiResponse<EntityId>> Handle(SignUp request, CancellationToken cancellationToken) {
		// validate

		// Validate if user exist

		// Create the user

		var user = User.Create(request.UserName);

		user.SetHashPassword(_passwordManager.Secure(request.Password));

		// Save to DB and return UserId
		var userId = new EntityId(0);

		// Return ApiResult
		return Task.FromResult(ApiResponse.Success(userId));
	}
}
