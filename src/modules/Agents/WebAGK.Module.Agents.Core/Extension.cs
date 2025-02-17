using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAGK.Module.Agents.Core;
internal static class Extension
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{
		return services;
	}
}
