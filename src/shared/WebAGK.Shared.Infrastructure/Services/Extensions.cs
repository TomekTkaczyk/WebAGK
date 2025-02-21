using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAGK.Shared.Infrastructure.Services;
internal static class Extensions
{
	public static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHostedService<EmailBackgroundService>();

		return services;
	}
}
