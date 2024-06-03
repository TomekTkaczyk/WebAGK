using AGK.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGK.DataAccess.Configurations;
internal abstract class ActiveStatusConfiguration<TEntity> : EntityConfiguration<TEntity>,IEntityTypeConfiguration<TEntity>  where TEntity : ActiveStatusEntity
{
	public override void Configure(EntityTypeBuilder<TEntity> builder) {
		base.Configure(builder);

		builder.Property(x => x.ActiveStatus)
			.HasConversion<BoolToZeroOneConverter<int>>();
	}
}
