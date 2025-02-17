using MediatR;

namespace WebAGK.Module.Users.UseCases.Commands.UpdatePermissions;
internal sealed record UpdatePermissionsCommand : IRequest
{
	public Guid Id { get; set; }
	public string Role { get; set; }
	public bool IsActive { get; set; }
	public IDictionary<string, IEnumerable<string>> Permissions { get; set; }
}
