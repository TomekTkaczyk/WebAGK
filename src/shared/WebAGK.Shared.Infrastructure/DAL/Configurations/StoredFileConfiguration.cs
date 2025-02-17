using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAGK.Shared.Abstractions.Entities;
using System.Text.Json;

namespace WebAGK.Shared.Infrastructure.DAL.Configurations;
internal class StoredFileConfiguration : IEntityTypeConfiguration<StoredFile>
{
	private static readonly JsonSerializerOptions SerializerOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	public void Configure(EntityTypeBuilder<StoredFile> builder)
	{
		builder.HasIndex(x => x.FileStorageName).IsUnique();
	}
}
