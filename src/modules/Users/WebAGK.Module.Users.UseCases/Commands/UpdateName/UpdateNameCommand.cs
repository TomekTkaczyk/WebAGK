using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.UpdateName;
internal sealed record UpdateNameCommand : IRequest
{
	public Guid Id { get; init; }

	public string FirstName { get; init; }

	public string LastName { get; init; }
}
