using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.DAL;
internal class AgentsDbContext(DbContextOptions<AgentsDbContext> options, ILogger<AgentsDbContext> logger) : DbContext(options)
{
	public DbSet<Agent> Agents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("Users");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		Database.Migrate();
		logger.LogInformation("Database migration {DbContext}", this.GetType().Name.Replace("DbContext", ""));
	}
}
