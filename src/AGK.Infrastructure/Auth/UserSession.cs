using AGK.Domain.Services;

namespace AGK.Infrastructure.Auth;
public sealed class UserSession(string userName = default, bool isAuthenticated = default)
{
	public string UserName { get; init; } = userName;

	public bool IsAuthenticated { get; init; } = isAuthenticated;
}
