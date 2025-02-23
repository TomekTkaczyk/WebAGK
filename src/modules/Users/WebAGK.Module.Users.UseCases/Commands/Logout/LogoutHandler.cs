using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.Logout;
internal class LogoutHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork) 
	: IRequestHandler<LogoutCommand>
{
	public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository
		.Get(new ByIdSpecification<User>(request.Id))
		.SingleOrDefaultAsync(cancellationToken)
		?? throw new InvalidCredentialsException();

		_user.RefreshToken = null;

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
