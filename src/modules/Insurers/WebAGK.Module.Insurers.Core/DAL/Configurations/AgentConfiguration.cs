using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent> {
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        // Podstawowa konfiguracja
        builder.ToTable("Agents"); // Możesz zmienić nazwę tabeli
        builder.HasKey(a => a.Id); // Klucz główny

        // Kolumna Name
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100); // Maksymalna długość dla imienia
    }
}