using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Shared.Infrastructure.ValueObjects;

namespace WebAGK.Module.Agents.Core.DAL.Configurations;

internal class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
	private static readonly JsonSerializerOptions SerializerOptions = new() {
		PropertyNameCaseInsensitive = true,
		WriteIndented = false
	};
	
	public void Configure(EntityTypeBuilder<Agent> builder) {
	
		builder.Property(x => x.Email)
			.HasConversion(
				email => (string)email,
				value => value);

		builder.Property(x => x.PersonalId)
			.HasConversion(
				id => (string)id,
				value => value);

		builder.Property(x => x.TaxId)
			.HasConversion(
				id => (string)id,
				value => value);
		
		builder.Property(x => x.RpuId)
			.HasConversion(
				id => (string)id,
				value => value);

		builder.Property(x => x.Address)
			.HasConversion(
				address => JsonSerializer.Serialize(address, SerializerOptions),
				value => JsonSerializer.Deserialize<Address>(value, SerializerOptions));
		
		builder.HasIndex(x => x.PersonalId)
			.HasFilter(@"PersonalId IS NOT NULL")
			.IsUnique();
		
		builder.HasIndex(x => x.TaxId)
			.HasFilter(@"TaxId IS NOT NULL")
			.IsUnique();
	}
}
