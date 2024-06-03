using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Domain.Services;
using AGK.Infrastructure.Auth;
using AGK.Infrastructure.Repositories;
using AGK.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AGK.Infrastructure;

public static class DependencyInjections
{
	private const string _dbSectionName = "Database";
	private const string _appSectionName = "App";
	private const string _authSectionName = "Auth";

	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

		var dbOptions = configuration.GetOptions<DatabaseOptions>(_dbSectionName);
		var appOptions = configuration.GetOptions<AppOptions>(_appSectionName);
		var authOptions = configuration.GetOptions<AuthOptions>(_authSectionName);

		var dllFiles = Directory.GetFiles(
			AppDomain.CurrentDomain.BaseDirectory,
			appOptions.Prefix + "*.dll");

		foreach(string filePath in dllFiles) {
			AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
		}

		var loadedAssemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => x.GetName().Name.StartsWith(appOptions.Prefix, StringComparison.CurrentCultureIgnoreCase))
			.ToArray();

		if(loadedAssemblies.Length == 0) {
			throw new Exception($"Empty list of assembly with prefix {appOptions.Prefix}");
		}
											   
		services.AddMediatR((cfg) => {
			cfg.RegisterServicesFromAssemblies(loadedAssemblies);
		});


		services.AddSingleton<IClock, Clock>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.AddScoped<DataInitializer>();
		
		services.AddAuth();
		services.AddDbContext(dbOptions);
		services.AddRepositories(loadedAssemblies);

		return services;
	}

	public static WebApplication UseInfrastructure (this WebApplication app) {

		// Configure the HTTP request pipeline.
		// app.UseAuthentication();
		// app.UseAuthorization();

		if(app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		return app;
	}

	public static void SeedData(this WebApplication app) {

		using(var scope = app.Services.CreateScope()) {
			var dataInitializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
			try {
				dataInitializer.UserInit();
			}
			catch(Exception ex) {
				Console.WriteLine(ex.ToString());
				throw;
			}
		}
	}

	public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T: class, new() {

		var options = new T();
		configuration.GetSection(sectionName).Bind(options);

		return options;
	}
}
