using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Employees.Core;
using WebAGK.Module.Employees.Core.DAL;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Employees.Api;

internal class EmployeeModule : IModule
{
	public const string BasePath = "employees-module";

	public string Name { get; } = "Employees";

	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = [];

	public void Register(IServiceCollection services, IConfiguration configuration)
	{
		services.AddCore(configuration);

		services.AddMediatR(cfg =>
		{
			var _assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => {
				var _name = x.GetName().Name;
				return _name != null && _name.StartsWith("WebAGK.Module.Employees.",
					StringComparison.CurrentCultureIgnoreCase);
			})
			.ToArray();
			cfg.RegisterServicesFromAssemblies(_assemblies);
		});
	}

	public void Use(IApplicationBuilder app)
	{
		app.MigrateDatabase<EmployeesDbContext>();
	}
}
