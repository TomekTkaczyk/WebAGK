using Microsoft.Extensions.DependencyInjection;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Shared.Infrastructure.Contexts;

internal static class Extensions
{
	public static IServiceCollection AddContexts(this IServiceCollection services)
	{
		services.AddHttpContextAccessor();
		services.AddSingleton<IContextFactory, ContextFactory>();
		services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());

		return services;
	} 
}
