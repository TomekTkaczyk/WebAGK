using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGK.DataAccess.Configurations;

internal sealed class UserEntityConfiguration : EntityConfiguration<User>, IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder) {

		BaseConfigure(builder);

		builder.Property(x => x.Username)
			.HasConversion(x => x.Value, x => new(x));

		builder.Property(x => x.Firstname)
			.HasConversion(x => x.Value, x => new(x));

		builder.Property(x => x.Lastname)
			.HasConversion(x => x.Value, x => new(x));

		builder.Property(x => x.Email)
			.HasConversion(x => x.Value, x => new(x));

		builder.HasIndex(x => x.Username)
			.HasDatabaseName("UsersIX_UserName")
			.IsUnique();

		builder.HasIndex(x => x.Email)
			.HasDatabaseName("UsersIX_Email")
			.IsUnique();

		builder.ToTable("Users");
	}
}
