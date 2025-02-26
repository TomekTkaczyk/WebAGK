using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.DeleteUser;

internal sealed class DeleteUserHandler(
    IUserRepository repository,
    IUserUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand> {
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        var _user = await repository
            .Get(new ByIdSpecification<User>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new UserNotFoundException(request.Id);
        repository.Delete(_user); 
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}