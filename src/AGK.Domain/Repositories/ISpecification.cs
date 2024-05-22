using System.Linq.Expressions;

namespace AGK.Domain.Repositories;

public interface ISpecification<T>
{
	// Filter Conditions
	Expression<Func<T, bool>> Criteria { get; }

	// Order by expression
	List<Tuple<Expression<Func<T, object>>, bool>> OrderByExpressions { get; }

	// Include collection to load related data
	List<Expression<Func<T, object>>> Includes { get; }

	Expression<Func<T, bool>> AsPredicateExpression();
}
