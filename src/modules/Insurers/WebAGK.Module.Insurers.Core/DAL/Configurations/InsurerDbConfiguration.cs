using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.DAL.DbModels;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class InsurerDbConfiguration : IEntityTypeConfiguration<InsurerDb> {
    public void Configure(EntityTypeBuilder<InsurerDb> builder) {
        builder.ToTable("InsurerDb");
        
        builder.HasKey(e => e.Id)
            .HasName(@"PK_InsurerDb");
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasMany(i => i.Structure)
            .WithOne(n => n.Insurer)
            .HasForeignKey(n => n.InsurerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}