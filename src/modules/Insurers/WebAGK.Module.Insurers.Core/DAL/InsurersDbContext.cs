using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL;

public class InsurersDbContext(DbContextOptions<InsurersDbContext> options) : DbContext(options){

    public DbSet<Agent> Agents { get; set; }
    public DbSet<InsurerDb> Insurers { get; set; }
    public DbSet<NodeDb> Nodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("Insurers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
