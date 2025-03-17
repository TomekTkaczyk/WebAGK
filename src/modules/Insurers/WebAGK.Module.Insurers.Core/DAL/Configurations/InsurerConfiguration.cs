using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class InsurerConfiguration : IEntityTypeConfiguration<Insurer> {
    public void Configure(EntityTypeBuilder<Insurer> builder)
    {
        builder.ToTable("Insurers");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        builder.HasMany(i => i.Structure)
            .WithOne(n => n.Insurer)
            .HasForeignKey(n => n.InsurerId)
            .IsRequired();

        
        // builder.OwnsMany(n => n.Structure, nodeBuilder => {
        //     nodeBuilder.ToTable("Structure");
        //     nodeBuilder.HasKey(x => new { x.InsurerId, ValueId = x.AgentId });
        //     
        //     nodeBuilder.WithOwner()
        //         .HasForeignKey(n => n.InsurerId);
        //     
        //     nodeBuilder.HasOne(n => n.Agent)
        //         .WithMany()
        //         .HasForeignKey(n => n.AgentId)
        //         .IsRequired();
        //     
        //     nodeBuilder.HasOne(n => n.Parent)
        //         .WithMany()
        //         .HasForeignKey(n => n.ParentId)
        //         .IsRequired(false);
        //     
        //     nodeBuilder.Property(n => n.Left);
        //     nodeBuilder.Property(n => n.Right);
        // });
    }
}