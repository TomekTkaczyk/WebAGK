using AGK.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AGK.DataAccess.Migrations;

public static class Extensions
{
	public static WebApplication PerformMigrations(this WebApplication app) {

		using(var scope = app.Services.CreateScope()) {
			var dbContext = scope.ServiceProvider.GetRequiredService<AgkDbContext>();
			try {
				dbContext.Database.Migrate();

			}
			catch(Exception ex) {
				Console.WriteLine(ex.ToString());
				throw;
			}
		}

		return app;
	}
}
