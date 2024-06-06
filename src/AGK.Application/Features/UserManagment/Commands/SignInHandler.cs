using AGK.Application.Reponse;
using AGK.Application.Services;
using AGK.Domain.Exceptions.Users;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;
internal sealed class SignInHandler(
	IUserManager userManager,
	IPasswordManager passwordManager) 
	: IRequestHandler<SignIn, ApiResponse>
{
	private readonly IUserManager _userManager = userManager;
	private readonly IPasswordManager _passwordManager = passwordManager;

	public async Task<ApiResponse> Handle(SignIn request, CancellationToken cancellationToken) {

		var user = await _userManager.FindByUserNameAsync(request.UserName, cancellationToken);
		if(user == null) {
			var userByEmail = await _userManager.FindByEmailAsync(request.UserName, cancellationToken) 
				?? throw new UserNotFoundException();
			user = userByEmail;
		}

		if(_passwordManager.VerifyHashedPassword(request.Password, user.PasswordHash)) {
			throw new InvalidPasswordException();
		}

		return ApiResponse.Success();
	}
}
