using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

internal class StructureConfiguration : IEntityTypeConfiguration<Structure>{
    public void Configure(EntityTypeBuilder<Structure> builder) {
        builder.ToTable("Structures");
        builder.HasKey(n => n.Id);
        

    }
}