using AGK.DataAccess;
using AGK.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Reflection;

namespace AGK.Infrastructure.Repositories;
internal static class Extensions
{

	internal static IServiceCollection AddDbContext(this IServiceCollection services, DatabaseOptions dbOptions) {

		services.AddDbContext<AgkDbContext>(options => {
			options.EnableDetailedErrors();
			if(services.BuildServiceProvider().GetService<IHostEnvironment>().IsDevelopment()) {
				options.EnableSensitiveDataLogging();
			}
			options.UseMySql(
				dbOptions.ConnectionString,
				new MariaDbServerVersion(ServerVersion.AutoDetect(dbOptions.ConnectionString)),
				b => {
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				});
		});

		return services;
	}

	internal static IServiceCollection AddRepositories(this IServiceCollection services, Assembly[] assemblies) {

		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.Scan(scan => {
			scan
				.FromAssemblies(assemblies)
				.AddClasses(classes => classes.Where(type => type.GetInterfaces().Any(i =>
					i.IsGenericType &&
					(i.GetGenericTypeDefinition() == typeof(IRepository<>)))))
				.AsImplementedInterfaces()
				.WithScopedLifetime();
		});

		return services;
	}
}
