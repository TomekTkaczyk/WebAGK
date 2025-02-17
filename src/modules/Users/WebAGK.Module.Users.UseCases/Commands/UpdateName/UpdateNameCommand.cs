using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.UpdateName;
internal sealed record UpdateNameCommand : IRequest
{
	public Guid Id { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }
}
