using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Shared.Infrastructure.Database;
using WebAGK.Module.Employees.Core.DAL;
using WebAGK.Module.Employees.Core.DAL.Repositories;
using WebAGK.Module.Employees.Core.Policies;
using WebAGK.Module.Employees.Core.Repositories;
using WebAGK.Module.Employees.Core.Services;

namespace WebAGK.Module.Employees.Core;

internal static class Extensions
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddDatatabase<EmployeesDbContext>(configuration);
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
		services.AddSingleton<IEmployeeDeletionPolicy, EmployeeDeletionPolicy>();
		services.AddScoped<IEmployeeService, EmployeeService>();

		return services;
	}
}
