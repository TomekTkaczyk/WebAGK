using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DAL;

internal class InsurersDbContext(DbContextOptions<InsurersDbContext> options) : DbContext(options){

    public DbSet<Agent> Agents { get; set; }
    public DbSet<Insurer> Insurers { get; set; }
    public DbSet<Node<Agent>> Nodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
        optionsBuilder
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("Insurers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
