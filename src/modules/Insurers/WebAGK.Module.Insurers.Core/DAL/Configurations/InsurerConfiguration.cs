using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class InsurerConfiguration : IEntityTypeConfiguration<Insurer>{
    public void Configure(EntityTypeBuilder<Insurer> builder) {
    }
}
