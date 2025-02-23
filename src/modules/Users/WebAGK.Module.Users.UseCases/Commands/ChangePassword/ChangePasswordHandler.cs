using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.ChangePassword;
internal class ChangePasswordHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork,
	IPasswordHasher<User> passwordHasher) : IRequestHandler<ChangePasswordCommand>
{
	public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository
			.Get( new ByIdSpecification<User>(request.Id))
			.SingleOrDefaultAsync(cancellationToken) 
		            ?? throw new InvalidCredentialsException();

		if(passwordHasher.VerifyHashedPassword(default, _user.Password, request.CurrentPassword) is not PasswordVerificationResult.Success) {
			throw new InvalidPasswordException();
		}

		var _password = passwordHasher.HashPassword(default, request.Password);
		_user.Password = _password;

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
