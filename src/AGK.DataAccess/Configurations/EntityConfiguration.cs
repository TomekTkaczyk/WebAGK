using AGK.DataAccess.Conversions;
using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGK.DataAccess.Configurations;
internal abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder) {

		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.HasConversion<EntityIdConverter>()
			.ValueGeneratedOnAdd();

		builder.Property(u => u.ConcurrencyStamp)
			.IsConcurrencyToken();

		builder.Property(x => x.CreatedById)
			.HasConversion<EntityIdConverter>();

		builder.Property(x => x.ModifiedById)
			.HasConversion<EntityIdConverter>();

		builder.HasIndex(x => x.CreatedById)
			.HasDatabaseName("UsersIX_CreatedById")
			.IsUnique(false);

		builder.HasOne(x => x.CreatedBy)
		   .WithMany()
		   .HasForeignKey(x => x.CreatedById)
		   .HasConstraintName("FK_CreatedById")
		   .OnDelete(DeleteBehavior.Restrict)
		   .IsRequired(false);

		builder.HasIndex(x => x.ModifiedById)
			.HasDatabaseName("UsersIX_ModifiedById")
			.IsUnique(false);

		builder.HasOne(x => x.ModifiedBy)
			.WithMany()
			.HasForeignKey(x => x.ModifiedById)
		    .HasConstraintName("FK_ModifiedById")
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired(false);
	}

}
