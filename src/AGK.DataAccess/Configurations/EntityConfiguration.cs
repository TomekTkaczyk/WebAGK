using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGK.DataAccess.Configurations;
internal abstract class EntityConfiguration<TEntity> where TEntity : BaseEntity
{
	protected static void BaseConfigure(EntityTypeBuilder<TEntity> builder) {

		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.HasConversion(x => x.Value, x => new(x))
			.ValueGeneratedOnAdd();

		builder.Property(u => u.ConcurrencyStamp)
			.HasConversion(x => x.ToString(), x => new(x))
			.IsConcurrencyToken()
			.ValueGeneratedOnAddOrUpdate();

		builder.Property(x => x.CreatedById)
			.HasConversion(x => x.Value, x => new(x));

		builder.Property(x => x.ModifiedById)
			.HasConversion(x => x.Value, x => new(x));

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
