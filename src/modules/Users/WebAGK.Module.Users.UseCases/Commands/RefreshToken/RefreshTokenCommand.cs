using MediatR;
using WebAGK.Shared.Abstractions.Auth;

namespace WebAGK.Module.Users.UseCases.Commands.RefreshToken;
internal sealed record RefreshTokenCommand(string Token) : IRequest<JsonWebToken> { }
