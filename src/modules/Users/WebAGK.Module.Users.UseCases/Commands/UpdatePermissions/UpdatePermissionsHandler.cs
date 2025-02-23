using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.UpdatePermissions;
internal class UpdatePermissionsHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork) 
	: IRequestHandler<UpdatePermissionsCommand>
{
	public async Task Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken) {
		var _user = await repository
	        .Get(new ByIdSpecification<User>(request.Id))
	        .SingleOrDefaultAsync(cancellationToken)
	        ?? throw new UserNotFoundException(request.Id);
		_user.Role = request.Role;
		_user.IsActive = request.IsActive;
		_user.Permissions = request.Permissions;

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}
}
