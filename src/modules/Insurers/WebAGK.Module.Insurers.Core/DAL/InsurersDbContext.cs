using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DAL.Repositories;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL;

public class InsurersDbContext(DbContextOptions<InsurersDbContext> options) : DbContext(options){
    
    public DbSet<Insurer> Insurers { get; set; }
    public DbSet<StructureNodeDbEntity> Agents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("Insurers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
