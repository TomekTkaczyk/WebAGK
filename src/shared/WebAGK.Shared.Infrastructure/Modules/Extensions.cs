using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAGK.Shared.Abstractions.Modules;

namespace WebAGK.Shared.Infrastructure.Modules;
public static class Extensions
{
	internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
	{
		var moduleInfoProvider = new ModuleInfoProvider();
		var moduleInfo = modules?.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? [])) ?? [];

		moduleInfoProvider.MolueInfos.AddRange(moduleInfo);

		services.AddMediatR(cfg => {
			cfg.RegisterServicesFromAssemblies(modules.Select(x => x.GetType().Assembly).ToArray());
		});

		services.AddSingleton(moduleInfoProvider);

		return services;
	}

	public static IHostBuilder ConfigureModules(this IHostBuilder builder)
		=> builder.ConfigureAppConfiguration((ctx, cfg) => {

			var settingsFiles = GetSettings("*");
			foreach(var settings in GetSettings("*")) {
				cfg.AddJsonFile(settings);
			}

			foreach(var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}")) {
				cfg.AddJsonFile(settings);
			}

			IEnumerable<string> GetSettings(string pattern)
			=> Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, 
				$"module.{pattern}.json", SearchOption.AllDirectories);
		});
}
