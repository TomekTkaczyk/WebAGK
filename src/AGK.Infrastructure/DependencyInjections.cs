using AGK.Domain.Services;
using AGK.Infrastructure.Auth;
using AGK.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AGK.Infrastructure;

public static class DependencyInjections
{
	
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

		var section = configuration.GetSection("App");
		services.Configure<AppOptions>(section);

		services.AddSingleton<IClock,Clock>();

		services.AddMediatR((cfg) => {
			cfg.RegisterServicesFromAssembly(Application.AssemblyReference.ApplicationAssembly);
		});

		services.AddAuth(configuration);

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

	public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T: class, new() {

		var options = new T();
		var section = configuration.GetSection(sectionName);
		section.Bind(options);

		return options;
	}
}
