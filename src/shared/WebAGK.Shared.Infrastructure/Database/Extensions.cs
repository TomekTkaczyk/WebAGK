using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace WebAGK.Shared.Infrastructure.Database;
public static class Extensions
{
	private const string _dbSectionName = "Postgres";

	public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {

		var dbOptions = configuration.GetOptions<DatabaseOptions>(_dbSectionName);
		services.AddSingleton(dbOptions);
		EnsureDatabaseExists(dbOptions.ConnectionString);

		return services;
	}

	public static IServiceCollection AddDatatabase<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext {
		var options = configuration.GetOptions<DatabaseOptions>(_dbSectionName);
		services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

		return services;
	}

	private static void EnsureDatabaseExists(string connectionString)
	{
		var builder = new NpgsqlConnectionStringBuilder(connectionString);
		var databaseName = builder.Database;
		builder.Database = "postgres"; // Połącz się z bazą systemową

		using var connection = new NpgsqlConnection(builder.ToString());
		connection.Open();

		using var cmd = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", connection);
		var exists = cmd.ExecuteScalar();

		if(exists == null)
		{
			using var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\"", connection);
			createCmd.ExecuteNonQuery();
			Console.WriteLine($"Database '{databaseName}' created.");
		}
	}

}
