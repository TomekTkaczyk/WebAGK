using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL;

public class InsurersDbContext(DbContextOptions<InsurersDbContext> options) : DbContext(options){
    
    DbSet<Insurer> Insurers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.HasDefaultSchema("Insurers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
