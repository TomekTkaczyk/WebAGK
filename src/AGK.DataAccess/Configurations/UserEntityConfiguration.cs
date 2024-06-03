using AGK.DataAccess.Conversions;
using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGK.DataAccess.Configurations;

internal sealed class UserEntityConfiguration : ActiveStatusConfiguration<User>, IEntityTypeConfiguration<User>	
{
	public override void Configure(EntityTypeBuilder<User> builder) {

		base.Configure(builder);

		builder.Property(x => x.UserName)
			.HasConversion<NameConverter>()
			.IsRequired();

		builder.Property(x => x.FirstName)
			.HasConversion<NameConverter>();

		builder.Property(x => x.LastName)
			.HasConversion<NameConverter>();

		builder.Property(x => x.Email)
			.HasConversion<EmailConverter>();

		builder.HasIndex(x => x.UserName)
			.HasDatabaseName("UsersIX_UserName")
			.IsUnique();

		builder.HasIndex(x => x.Email)
			.HasDatabaseName("UsersIX_Email")
			.IsUnique();

		builder.ToTable("Users");
	}
}
