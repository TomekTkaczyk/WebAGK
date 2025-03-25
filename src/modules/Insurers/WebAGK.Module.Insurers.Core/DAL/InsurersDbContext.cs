using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL;

internal class InsurersDbContext(DbContextOptions<InsurersDbContext> options) : WebAgkDbContext(options){

    public DbSet<Agent> Agents { get; set; }
    public DbSet<Insurer> Insurers { get; set; }
    public DbSet<Structure> Structures { get; set; }
    public DbSet<Node> Nodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Insurers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        // foreach (var _entityType in modelBuilder.Model.GetEntityTypes()) {
        //     var _rowVersionProperty = _entityType
        //         .GetProperties()
        //         .FirstOrDefault(p => p.Name == "Version");
        //
        //     if (_rowVersionProperty is null) continue;
        //     
        //     _rowVersionProperty.SetColumnType("uint");
        //     _rowVersionProperty.ValueGenerated = ValueGenerated.OnAddOrUpdate;
        //     _rowVersionProperty.IsConcurrencyToken = true;
        // }
    }
}
