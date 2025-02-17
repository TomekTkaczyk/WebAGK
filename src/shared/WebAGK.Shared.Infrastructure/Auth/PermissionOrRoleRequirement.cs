using Microsoft.AspNetCore.Authorization;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class PermissionOrRoleRequirement(
	IEnumerable<string> requiredPermissions, 
	IEnumerable<string> requiredRoles) : IAuthorizationRequirement
{
	public List<string> RequiredPermissions { get; } = new List<string>(requiredPermissions);
	public List<string> RequiredRoles { get; } = new List<string>(requiredRoles);
}
