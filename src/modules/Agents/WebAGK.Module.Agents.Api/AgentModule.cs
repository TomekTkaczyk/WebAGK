using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Agents.Core;
using WebAGK.Shared.Abstractions.Modules;

namespace WebAGK.Modules.Agents.Api;
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
			var assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => x.GetName().Name.StartsWith("WebAGK.Module.Agents.", StringComparison.CurrentCultureIgnoreCase))
			.ToArray();
			cfg.RegisterServicesFromAssemblies(assemblies);
		});
	}

	public void Use(IApplicationBuilder app)
	{
	}
}
