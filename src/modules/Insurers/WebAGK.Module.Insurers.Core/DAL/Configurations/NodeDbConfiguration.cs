using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.DAL.DbModels;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class NodeDbConfiguration : IEntityTypeConfiguration<NodeDb> {
    public void Configure(EntityTypeBuilder<NodeDb> builder) {
        builder.ToTable("NodeDb");
        
        builder.HasKey(e => e.Id);
        
        // Relacja do rodzica (self-referencing)
        builder.HasOne(n => n.Parent)
            .WithMany(p => p.Nodes)
            .HasForeignKey(n => n.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacja do ubezpieczyciela
        builder.HasOne(n => n.Insurer)
            .WithMany(i => i.Structure)
            .HasForeignKey(n => n.InsurerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacja do agenta
        builder.HasOne(n => n.Agent)
            .WithMany()
            .HasForeignKey(n => n.AgentId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indeks dla modelu nested set
        builder.HasIndex(n => new { n.Left, n.Right });
    }
}