using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

internal class InsurerConfiguration : IEntityTypeConfiguration<Insurer> {
    public void Configure(EntityTypeBuilder<Insurer> builder)
    {
        builder.ToTable("Insurers");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        builder.HasOne(x => x.Structure)
            .WithOne()
            .HasForeignKey<Structure>(i => i.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}