using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Employees.Core;
using WebAGK.Shared.Abstractions.Modules;

namespace WebAGK.Module.Employees.Api;
internal class EmployeeModule : IModule
{
	public const string BasePath = "employees-module";

	public string Name { get; } = "Employees";

	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = ["Uprawnienie1", "Uprawnienie2"];

	public void Register(IServiceCollection services, IConfiguration configuration)
	{
		services.AddCore(configuration);

		services.AddMediatR(cfg =>
		{
			var assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => x.GetName().Name.StartsWith("WebAGK.Module.Employees.", StringComparison.CurrentCultureIgnoreCase))
			.ToArray();
			cfg.RegisterServicesFromAssemblies(assemblies);
		});
	}

	public void Use(IApplicationBuilder app)
	{
	}
}
