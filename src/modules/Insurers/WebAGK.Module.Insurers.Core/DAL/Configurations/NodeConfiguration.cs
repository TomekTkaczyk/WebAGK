using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

internal class NodeConfiguration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.ToTable("Nodes");
        builder.HasKey(n => n.Id);
        
        builder.HasOne(x => x.Agent)
            .WithMany()
            .HasForeignKey(x => x.AgentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Structure)
            .WithMany(x => x.Nodes)
            .HasForeignKey(x => x.StructureId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Nodes)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}