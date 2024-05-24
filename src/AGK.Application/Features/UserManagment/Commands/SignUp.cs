using AGK.Application.Reponse;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

public sealed record SignUp(
	string UserName,
	string Email,
	string Password) : IRequest<ApiResponse<EntityId>>;


