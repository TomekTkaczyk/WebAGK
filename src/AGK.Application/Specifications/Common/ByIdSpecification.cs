using AGK.Domain.Entities;

namespace AGK.Application.Specifications.Common;

internal sealed class ByIdSpecification<TEntity>(int id)
	: Specification<TEntity>(entity => entity.Id == id) where TEntity : BaseEntity { }
