using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Agents.Core;
using WebAGK.Module.Agents.Core.DAL;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Agents.Api;
internal class AgentModule : IModule
{
	public const string BasePath = "agents-module";

	public string Name { get; } = "Agents";

	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = ["AgentManager"];

	public void Register(IServiceCollection services, IConfiguration configuration)
	{
		services.AddCore(configuration);

		services.AddMediatR(cfg =>
		{
			var _assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => {
				var _name = x.GetName().Name;
				return _name != null && _name.StartsWith("WebAGK.Module.Agents.",
					StringComparison.CurrentCultureIgnoreCase);
			})
			.ToArray();
			cfg.RegisterServicesFromAssemblies(_assemblies);
		});
	}

	public void Use(IApplicationBuilder app)
	{
		app.MigrateDatabase<AgentsDbContext>();
	}
}
