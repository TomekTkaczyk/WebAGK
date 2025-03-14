using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Users.Core.Entities;

namespace WebAGK.Module.Users.Core.DAL;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
			optionsBuilder
				.EnableSensitiveDataLogging()
				.LogTo(Console.WriteLine, LogLevel.Information);
		}
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("Users");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
