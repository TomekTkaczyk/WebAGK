using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Insurers.Core;
using WebAGK.Module.Insurers.Core.DAL;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Insurers.Api;

internal class InsurerModule : IModule
{
	public const string BasePath = "insurers-module";

	public string Name { get; } = "Insurers";

	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = ["InsurerManager"];

	public void Register(IServiceCollection services, IConfiguration configuration)
	{
		services.AddCore(configuration);

		services.AddMediatR(cfg =>
		{
			var _assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => {
				var _name = x.GetName().Name;
				return _name != null && _name.StartsWith("WebAGK.Module.Insurers.",
					StringComparison.CurrentCultureIgnoreCase);
			})
			.ToArray();
			cfg.RegisterServicesFromAssemblies(_assemblies);
		});
	}

	public void Use(IApplicationBuilder app) {
		app.MigrateDatabase<InsurersDbContext>();
	}
}
