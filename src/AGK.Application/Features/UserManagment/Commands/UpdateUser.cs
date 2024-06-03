using AGK.Application.Reponse;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

public sealed record UpdateUser(
	EntityId Id,
	string UserName,
	string FirstName,
	string LastName) : IRequest<ApiResponse>;