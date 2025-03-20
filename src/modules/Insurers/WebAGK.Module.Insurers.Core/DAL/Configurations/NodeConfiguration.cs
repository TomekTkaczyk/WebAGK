using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

internal class NodeConfiguration : IEntityTypeConfiguration<Node<Agent>>
{
    public void Configure(EntityTypeBuilder<Node<Agent>> builder)
    {
        builder.ToTable("Nodes");
        
        builder.HasKey(n => n.Id);
        
        // builder.HasOne(n => n.Value)
        //     .WithMany()
        //     .HasForeignKey(n => n.Value.Id)
        //     .IsRequired();
        //
        // builder.HasOne(n => n.Parent)
        //     .WithMany()
        //     .HasForeignKey(n => n.Parent.Id)
        //     .IsRequired(false)
        //     .OnDelete(DeleteBehavior.Restrict); // Brak kaskady, Parent może być NULL
    }
}