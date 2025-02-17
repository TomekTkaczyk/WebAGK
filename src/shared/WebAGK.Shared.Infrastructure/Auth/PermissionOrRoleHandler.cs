using Microsoft.AspNetCore.Authorization;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class PermissionOrRoleHandler : AuthorizationHandler<PermissionOrRoleRequirement>
{
	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		PermissionOrRoleRequirement requirement)
	{
		if(context.User == null) {
			return Task.CompletedTask;
		}

		if(requirement.RequiredRoles.Count != 0) {
			var roles = context.User.Claims
				.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
				.Select(c => c.Value);
			if(roles.Any(r => requirement.RequiredRoles.Contains(r))) {
				context.Succeed(requirement);
				return Task.CompletedTask;
			}
		}
		if(requirement.RequiredPermissions.Count != 0) {
			var permissions = context.User.Claims
				.Where(c => c.Type == "permissions")
				.Select(c => c.Value);
			if(permissions.Any(p => requirement.RequiredPermissions.Contains(p))) {
				context.Succeed(requirement);
				return Task.CompletedTask;
			}
		}
		return Task.CompletedTask;
	}
}
