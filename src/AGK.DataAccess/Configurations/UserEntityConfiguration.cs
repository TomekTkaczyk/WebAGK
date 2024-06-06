using AGK.DataAccess.Conversions;
using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGK.DataAccess.Configurations;

internal sealed class UserEntityConfiguration : EntityConfiguration<User>, IEntityTypeConfiguration<User>	
{
	public override void Configure(EntityTypeBuilder<User> builder) {

		base.Configure(builder);

		builder.Property(x => x.UserName)
			.HasConversion<UserNameConverter>()
			.HasMaxLength(30)
			.IsRequired();

		builder.Property(x => x.FirstName)
			.HasMaxLength(100)
			.HasConversion<NameConverter>();

		builder.Property(x => x.LastName)
			.HasMaxLength(100)
			.HasConversion<NameConverter>();

		builder.Property(x => x.PhoneNumber)
			.HasMaxLength(50);

		builder.Property(x => x.PasswordHash)
			.HasMaxLength(200);

		builder.Property(x => x.Role)
			.HasConversion<RoleConverter>()
			.HasMaxLength(50);

		builder.Property(x => x.NormalizedName)
			.HasMaxLength(200);

		builder.Property(x => x.Email)
			.HasMaxLength(50)
			.HasConversion<EmailConverter>()
			.IsRequired();

		builder.HasIndex(x => x.UserName)
			.HasDatabaseName("UsersIX_UserName")
			.IsUnique();

		builder.HasIndex(x => x.Email)
			.HasDatabaseName("UsersIX_Email")
			.IsUnique();

		builder.ToTable("Users");
	}
}
