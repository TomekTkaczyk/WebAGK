using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace WebAGK.Shared.Infrastructure.Database;
public static class Extensions
{
	private const string DbSectionName = "Postgres";

	public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) {

		var _dbOptions = configuration.GetOptions<DatabaseOptions>(DbSectionName);
		services.AddSingleton(_dbOptions);
		EnsureDatabaseExists(_dbOptions.ConnectionString);

		return services;
	}

	public static IServiceCollection AddDatabase<T>(
		this IServiceCollection services, 
		IConfiguration configuration) where T : DbContext {
		var _options = configuration.GetOptions<DatabaseOptions>(DbSectionName);
		services.AddDbContext<T>(x => {
			x.UseNpgsql(_options.ConnectionString);
		});

		return services;
	}

	public static IApplicationBuilder MigrateDatabase<T>(this IApplicationBuilder app) where T : DbContext {
		using var _scope = app.ApplicationServices.CreateScope();
		var _serviceProvider = _scope.ServiceProvider;
		var _dbContext = _serviceProvider.GetService<T>();
		_dbContext.Database.Migrate();

		return app;
	}

	private static void EnsureDatabaseExists(string connectionString)
	{
		var _builder = new NpgsqlConnectionStringBuilder(connectionString);
		var _databaseName = _builder.Database;
		_builder.Database = "postgres"; // Połącz się z bazą systemową

		using var _connection = new NpgsqlConnection(_builder.ToString());
		_connection.Open();

		using var _cmd = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{_databaseName}'", _connection);
		var _exists = _cmd.ExecuteScalar();

		if (_exists != null) return;
		
		using var _createCmd = new NpgsqlCommand($"CREATE DATABASE \"{_databaseName}\"", _connection);
		_createCmd.ExecuteNonQuery();
		Console.WriteLine($"Database '{_databaseName}' created.");
	}
}
