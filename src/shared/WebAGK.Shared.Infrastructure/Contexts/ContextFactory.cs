using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Shared.Infrastructure.Contexts;

internal class ContextFactory(IHttpContextAccessor httpContextAccessor) : IContextFactory
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public IContext Create()
	{
		var _httpContext = _httpContextAccessor.HttpContext;
		return _httpContext is null ? Context.Empty : new Context(_httpContext);
	}
}
