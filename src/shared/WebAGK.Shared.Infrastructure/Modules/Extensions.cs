using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAGK.Shared.Abstractions.Modules;

namespace WebAGK.Shared.Infrastructure.Modules;
public static class Extensions
{
	internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
	{
		var _moduleInfoProvider = new ModuleInfoProvider();
		var _moduleInfo = modules?.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? [])) ?? [];

		_moduleInfoProvider.ModuleInfos.AddRange(_moduleInfo);

		services.AddMediatR(cfg => {
			if (modules != null) {
				cfg.RegisterServicesFromAssemblies(modules.Select(x => x.GetType().Assembly).ToArray());
			}
		});

		services.AddSingleton(_moduleInfoProvider);

		return services;
	}

	public static IHostBuilder ConfigureModules(this IHostBuilder builder)
		=> builder.ConfigureAppConfiguration((ctx, cfg) => {

			var _settingsFiles = GetSettings("*");
			foreach(var _settings in GetSettings("*")) {
				cfg.AddJsonFile(_settings);
			}

			foreach(var _settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}")) {
				cfg.AddJsonFile(_settings);
			}

			return;

			IEnumerable<string> GetSettings(string pattern)
			=> Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, 
				$"module.{pattern}.json", SearchOption.AllDirectories);
		});
}
