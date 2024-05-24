using AGK.Application.Auth;
using AGK.Application.Reponse;
using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

internal sealed class SignUpHandler(IUserManager userManager, IPasswordManager passwordManager) : IRequestHandler<SignUp, ApiResponse<EntityId>>
{
	private readonly IUserManager _userManager = userManager;
	private readonly IPasswordManager _passwordManager = passwordManager;

	public async Task<ApiResponse<EntityId>> Handle(SignUp request, CancellationToken cancellationToken = default) {
		// Validate

		// Validate if user exist

		// Create the user
		var user = User.Create(
				request.UserName,
				request.Email,
				_passwordManager.HashPassword(request.Password));

		user = await _userManager.CreateAsync(user,	cancellationToken);

		// Return ApiResult
		return ApiResponse.Success(user.Id);
	}
}
