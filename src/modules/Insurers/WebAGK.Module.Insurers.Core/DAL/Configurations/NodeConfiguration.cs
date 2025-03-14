using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class NodeConfiguration : IEntityTypeConfiguration<Node> {
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.ToTable("Structures");
        builder.HasKey(n => n.Id);

        builder.HasOne(n => n.Value)
            .WithMany()
            .HasForeignKey(n => n.ValueId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Parent)
            .WithMany(p => p.Nodes)
            .HasForeignKey(n => n.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(n => n.ParentId)
            .IsRequired(false);
    }
}