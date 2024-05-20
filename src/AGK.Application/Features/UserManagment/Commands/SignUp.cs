using AGK.Application.Reponse;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

public sealed record SignUp(
	string UserName,
	string Password) : IRequest<ApiResponse<EntityId>>;


