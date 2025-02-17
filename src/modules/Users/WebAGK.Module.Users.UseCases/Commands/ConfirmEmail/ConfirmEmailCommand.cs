using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.ConfirmEmail;
internal sealed record ConfirmEmailCommand(string Token) : IRequest { }
