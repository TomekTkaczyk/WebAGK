using Microsoft.AspNetCore.Http;

namespace AGK.Infrastructure.Auth;

public sealed class AuthorizedUserService(IHttpContextAccessor httpContextAccessor)
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public UserSession AuthorizedUser {
		get {
			if(_httpContextAccessor?.HttpContext == null) {
				return new UserSession();
			}

			var currentUser = new UserSession(0,
				_httpContextAccessor.HttpContext.User.Identity.Name,
				_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated);
			;

			return currentUser;
		}
	}
}
