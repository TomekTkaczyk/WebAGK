using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.UpdateName;
internal class UpdateNameHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork) : IRequestHandler<UpdateNameCommand>
{
	public async Task Handle(UpdateNameCommand request, CancellationToken cancellationToken)
	{
		var _user = await repository
			.Get(new ByIdSpecification<User>(request.Id))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new UserNotFoundException(request.Id);

		_user.FirstName = request.FirstName;
		_user.LastName = request.LastName;

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
