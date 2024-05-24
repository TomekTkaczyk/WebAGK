using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AGK.DataAccess;

public sealed class AgkDbContext(DbContextOptions<AgkDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
	public DbSet<User> Users { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(null);
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		
		if(!optionsBuilder.IsConfigured) {
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.Build();

			var dbOptions = new DatabaseOptions();

			configuration.GetSection("Database").Bind(dbOptions);

			optionsBuilder.UseMySql(
				dbOptions.ConnectionString,
				new MariaDbServerVersion(ServerVersion.AutoDetect(dbOptions.ConnectionString)),
				b => {
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				});
		}

		bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
		if(isDevelopment) {
			optionsBuilder.EnableSensitiveDataLogging();
			var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
			optionsBuilder.UseLoggerFactory(loggerFactory);
		}
	}
}
