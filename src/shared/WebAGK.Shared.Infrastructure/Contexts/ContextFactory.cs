using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Shared.Infrastructure.Contexts;

internal class ContextFactory(IHttpContextAccessor httpContextAccessor) : IContextFactory
{
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public IContext Create()
	{
		var httpContext = _httpContextAccessor.HttpContext;
		return httpContext is null ? Context.Empty : new Context(httpContext);
	}
}
