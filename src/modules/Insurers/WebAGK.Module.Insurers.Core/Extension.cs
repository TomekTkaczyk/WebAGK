using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Insurers.Core.DAL;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Insurers.Core;

internal static class Extension
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDatabase<InsurersDbContext>(configuration);

		return services;
	}
}
