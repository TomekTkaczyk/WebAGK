using AGK.Application.Reponse;
using AGK.Application.Services;
using AGK.Domain.Entities;
using AGK.Domain.Exceptions.Users;
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

		var user = User.Create(request.UserName, request.Email, _passwordManager.HashPassword(request.Password));

		var isUnique = await _userManager.IsUniqueAsync(user, cancellationToken);

		if (!isUnique) { 
			throw new UserAlreadyExistException();
		}

		var userAfterAdding = await _userManager.AddAsync(user, cancellationToken);

		return ApiResponse.Success(userAfterAdding.Id);
	}
}
