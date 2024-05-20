using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;

public sealed record UpdateUser(
	int Id,
	string Username,
	string Firstname,
	string Lastname) : IRequest;