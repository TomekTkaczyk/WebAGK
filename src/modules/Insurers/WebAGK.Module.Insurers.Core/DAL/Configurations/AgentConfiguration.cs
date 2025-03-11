using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent> {
    public void Configure(EntityTypeBuilder<Agent> builder) {
        
        builder.ToTable("Agent");
        
        builder.HasKey(x => x.Id)
            .HasName(@"AgentId");
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
    }
}