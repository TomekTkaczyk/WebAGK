using MediatR;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.UpdateName;
internal class UpdateNameHandler(
	IUserRepository repository) : IRequestHandler<UpdateNameCommand>
{
	public async Task Handle(UpdateNameCommand request, CancellationToken cancellationToken)
	{
		var user = await repository.GetAsync(request.Id, cancellationToken)
			?? throw new InvalidCredentialsException();

		user.FirstName = request.FirstName;
		user.LastName = request.LastName;

		await repository.UpdateAsync(user, cancellationToken);
	}
}
