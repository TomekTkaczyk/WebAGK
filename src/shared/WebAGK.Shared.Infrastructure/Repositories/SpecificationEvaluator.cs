using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions.Repositories;

namespace WebAGK.Shared.Infrastructure.Repositories;

public static class SpecificationEvaluator<T> where T : class
{
	public static IQueryable<T> GetQuery(IQueryable<T> sourceQuery, ISpecification<T> specification) {
		var _query = sourceQuery;

		if(specification is null) {
			return _query;
		}

		if(specification.Criteria is not null) {
			_query = _query.Where(specification.Criteria);
		}

		_query = specification.Includes.Aggregate(_query, (current, include) => current.Include(include));

		if(specification.OrderByExpressions.Count > 0) {
			var orderQueryable = specification.OrderByExpressions.First().Item2
				? _query.OrderByDescending(specification.OrderByExpressions.First().Item1)
				: _query.OrderBy(specification.OrderByExpressions.First().Item1);

			foreach(var expression in specification.OrderByExpressions.Skip(1)) {
				orderQueryable = expression.Item2
					? orderQueryable.ThenByDescending(expression.Item1)
					: orderQueryable.ThenBy(expression.Item1);
			}

			_query = orderQueryable;
		}

		return _query;
	}
}
