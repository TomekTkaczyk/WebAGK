using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Users.Core.Entities;

namespace WebAGK.Module.Users.Core.DAL;
internal class UsersDbContext(DbContextOptions<UsersDbContext> options, ILogger<UsersDbContext> logger) : DbContext(options)
{
	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema("Users");
		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		Database.Migrate();
		logger.LogInformation("Database migration {DbContext}", this.GetType().Name.Replace("DbContext", ""));
	}
}
