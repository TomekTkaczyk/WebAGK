using MediatR;
using WebAGK.Module.Users.Core.DTO;

namespace WebAGK.Module.Users.UseCases.Queries.GetUsers;
internal sealed record GetUsersQuery : IRequest<IReadOnlyList<UserProfileDto>> { }
