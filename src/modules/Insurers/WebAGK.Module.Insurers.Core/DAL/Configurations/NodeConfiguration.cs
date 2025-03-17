using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Types;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class NodeConfiguration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.ToTable("Structure");

        builder.HasKey(n => n.Id);

        builder.HasOne(n => n.Insurer)
            .WithMany(i => i.Structure)
            .HasForeignKey(n => n.InsurerId)
            .IsRequired();

        builder.HasOne(n => n.Agent)
            .WithMany()
            .HasForeignKey(n => n.AgentId)
            .IsRequired();

        builder.HasOne(n => n.Parent)
            .WithMany()
            .HasForeignKey(n => n.ParentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict); // Brak kaskady, Parent może być NULL
    }
}