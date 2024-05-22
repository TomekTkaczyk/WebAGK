using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

public sealed record UpdateUser(
	EntityId Id,
	string Username,
	string Firstname,
	string Lastname) : IRequest;