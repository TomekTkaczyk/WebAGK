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

        // Relacja między Insurer a Node
        builder.HasMany(i => i.Structure)
            .WithOne() // Node nie ma referencji do Insurer
            .HasForeignKey("InsurerId") // Klucz obcy w tabeli Node
            .OnDelete(DeleteBehavior.Cascade);
    }
}