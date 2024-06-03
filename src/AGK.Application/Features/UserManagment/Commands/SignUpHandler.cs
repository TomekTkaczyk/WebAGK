using AGK.Application.Reponse;
using AGK.Application.Services;
using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

internal sealed class SignUpHandler(
	IUserManager userManager,
	IPasswordManager passwordManager) 
	: IRequestHandler<SignUp, ApiResponse<EntityId>>
{
	private readonly IUserManager _userManager = userManager;
	private readonly IPasswordManager _passwordManager = passwordManager;

	public async Task<ApiResponse<EntityId>> Handle(SignUp request, CancellationToken cancellationToken = default) {
		// Validate

		// Validate if user exist
		if(( _userManager.FindByEmailAsync(request.Email, cancellationToken) != null) || (
			_userManager.FindByUserNameAsync(new Name(request.UserName), cancellationToken) != null)) {

		};

		// Create the user
		var user = User.Create(
				request.UserName,
				request.Email,
				_passwordManager.HashPassword(request.Password));

		try {
			user = await _userManager.CreateAsync(user,	cancellationToken);
		}
		catch(Exception ex) {

			throw;
		}

		// Return ApiResult
		return ApiResponse.Success(user.Id);
	}
}
