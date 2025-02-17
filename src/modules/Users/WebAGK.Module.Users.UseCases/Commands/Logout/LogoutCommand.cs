using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.Logout;
internal sealed record LogoutCommand(Guid Id) : IRequest { }
