using MediatR;
using WebAGK.Module.Users.Core.DTO;

namespace WebAGK.Module.Users.UseCases.Queries.GetUser;
internal sealed record GetUserQuery(Guid Id) : IRequest<UserDto> { }
