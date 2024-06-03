using Microsoft.AspNetCore.Http;

namespace AGK.Infrastructure.Auth;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public UserSession GetCurrentUser() {
		if(_httpContextAccessor?.HttpContext == null) {
			return new UserSession();
		}

		var currentUser = new UserSession(
			_httpContextAccessor.HttpContext.User.Identity.Name,
			_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated);

		return currentUser;
	}
}
