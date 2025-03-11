using System.Linq.Expressions;

namespace WebAGK.Shared.Infrastructure.Repositories;

public static class Extensions
{
	public static OrSpecification<T> Or<T>(this Specification<T> left, Specification<T> right) {
		return new OrSpecification<T>(left, right);
	}

	public static AndSpecification<T> And<T>(this Specification<T> left, Specification<T> right) {
		return new AndSpecification<T>(left, right);
	}

	public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQueryable, Specification<T> specification) {
		return inputQueryable.AsQueryable().Where(specification);
	}
	
	public static Expression<Func<TDestination, bool>> ConvertExpression<TSource, TDestination>(
		this Expression<Func<TSource, bool>> sourceExpression)
	{
		if (sourceExpression == null) return null;

		var _parameter = Expression.Parameter(typeof(TDestination), sourceExpression.Parameters[0].Name);
		var _body = new ExpressionRebinder<TSource, TDestination>(_parameter).Visit(sourceExpression.Body);
		return Expression.Lambda<Func<TDestination, bool>>(_body, _parameter);
	}

	public static Expression<Func<TDestination, object>> ConvertExpression<TSource, TDestination>(
		this Expression<Func<TSource, object>> sourceExpression)
	{
		if (sourceExpression == null) return null;

		var _parameter = Expression.Parameter(typeof(TDestination), sourceExpression.Parameters[0].Name);
		var _body = new ExpressionRebinder<TSource, TDestination>(_parameter).Visit(sourceExpression.Body);
		return Expression.Lambda<Func<TDestination, object>>(_body, _parameter);
	}
	
	public static string ToSnakeCase(this string input)
		=> string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
}
