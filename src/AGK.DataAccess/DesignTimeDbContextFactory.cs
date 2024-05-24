using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AGK.DataAccess;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AgkDbContext>
{
	private const string _dbSectionName = "Database";

	public AgkDbContext CreateDbContext(string[] args) {

		var configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.Build();

		DatabaseOptions dbOptions = new();

		configuration.GetSection(_dbSectionName).Bind(dbOptions);

		bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
		if(isDevelopment) {
			dbOptions.ConnectionString = "Server=localhost;Database=WebAGK;Uid=root;Pwd=123456;";

			Console.WriteLine($"Base directorys: {AppDomain.CurrentDomain.BaseDirectory}");
			Console.WriteLine($"ConnectionString: {dbOptions.ConnectionString}");
			Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
		}

		var optionsBuilder = new DbContextOptionsBuilder<AgkDbContext>();
		optionsBuilder.UseMySql(dbOptions.ConnectionString, 
			new MariaDbServerVersion(ServerVersion.AutoDetect(dbOptions.ConnectionString)),
				b => {
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				});

		return new AgkDbContext(optionsBuilder.Options);
	}
}

