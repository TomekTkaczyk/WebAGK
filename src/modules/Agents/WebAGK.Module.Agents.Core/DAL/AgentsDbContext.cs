using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.Core.DAL;

public class AgentsDbContext(DbContextOptions<AgentsDbContext> options) : DbContext(options)
{
	public DbSet<Agent> Agents { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.HasDefaultSchema("Agents");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}