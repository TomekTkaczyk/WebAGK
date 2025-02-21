using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Repositories;

public sealed class ByIdSpecification<TEntity>(Guid id)
	: Specification<TEntity>(entity => entity.Id == id) where TEntity : EntityBase { }
