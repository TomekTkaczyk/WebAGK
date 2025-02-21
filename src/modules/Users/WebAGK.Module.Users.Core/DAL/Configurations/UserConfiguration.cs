using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Users.Core.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	private static readonly JsonSerializerOptions SerializerOptions = new() {
		PropertyNameCaseInsensitive = true,		
		WriteIndented = false
	};

	public void Configure(EntityTypeBuilder<User> builder) {
		
		builder.Property(x => x.Password).IsRequired();
		builder.Property(x => x.Role).IsRequired();
		
		builder.Property(x => x.Email)
			.HasConversion(
				email => (string)email,
				value => value)
			.IsRequired();
		
		builder.Property(x => x.Permissions)
			.HasConversion(
				claims => JsonSerializer.Serialize(claims, SerializerOptions),
				value => JsonSerializer.Deserialize<IDictionary<string, IEnumerable<string>>>(value, SerializerOptions));

		builder.Property(x => x.Permissions).Metadata.SetValueComparer(
			new ValueComparer<IDictionary<string, IEnumerable<string>>>(
				(c1, c2) => c1.SequenceEqual(c2),
				c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
				c => c.ToDictionary(x => x.Key, x => x.Value)));
		
		builder.HasIndex(x => x.Name).IsUnique();
		builder.HasIndex(x => x.Email).IsUnique();
	}
}
