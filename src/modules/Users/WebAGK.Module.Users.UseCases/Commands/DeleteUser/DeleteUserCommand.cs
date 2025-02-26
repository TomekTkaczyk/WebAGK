using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id): IRequest;