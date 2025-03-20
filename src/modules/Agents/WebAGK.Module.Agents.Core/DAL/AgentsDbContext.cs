using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.DAL;

public class AgentsDbContext(
	DbContextOptions<AgentsDbContext> options) : DbContext(options)
{
	public DbSet<Agent> Agents { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
			optionsBuilder
				.EnableSensitiveDataLogging()
				.LogTo(Console.WriteLine, LogLevel.Information);
		}
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.HasDefaultSchema("Agents");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}