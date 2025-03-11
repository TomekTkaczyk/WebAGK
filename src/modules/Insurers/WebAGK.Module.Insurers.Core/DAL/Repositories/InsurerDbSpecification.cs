using WebAGK.Module.Insurers.Core.DAL.DbModels;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public class InsurerDbSpecification : Specification<InsurerDb>{
    public InsurerDbSpecification(ISpecification<Insurer> domainSpec)
        : base(domainSpec.Criteria.ConvertExpression<Insurer, InsurerDb>())
    {
        foreach (var _include in domainSpec.Includes)
        {
            AddInclude(_include.ConvertExpression<Insurer, InsurerDb>());
        }

        foreach (var _orderBy in domainSpec.OrderByExpressions)
        {
            AddOrderByExpression(_orderBy.Item1.ConvertExpression<Insurer, InsurerDb>(), _orderBy.Item2);
        }
    }
}