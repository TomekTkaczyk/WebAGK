using Microsoft.AspNetCore.Http;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Shared.Infrastructure.Contexts;

internal class Context : IContext
{
	public string Request { get; } = $"{Guid.NewGuid():N}";
	public string TraceId { get; }
	public IIdentityContext Identity { get; }

    public Context(HttpContext httpContext)
        : this(httpContext.TraceIdentifier, new IdentityContext(httpContext.User)) { }

    internal Context(string traceId, IIdentityContext identity)
    {
		TraceId = traceId;
		Identity = identity;
	}

    private Context() { }

    public static IContext Empty => new Context(); 
}
