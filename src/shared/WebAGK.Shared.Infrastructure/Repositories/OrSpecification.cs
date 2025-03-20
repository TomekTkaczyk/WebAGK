using System.Linq.Expressions;

namespace WebAGK.Shared.Infrastructure.Repositories;

public class OrSpecification<T>(params Specification<T>[] specifications) : Specification<T>
{
	private readonly Specification<T>[] _specifications = specifications 
		?? throw new ArgumentNullException(nameof(specifications));

	public override Expression<Func<T, bool>> AsPredicateExpression() {
		Expression<Func<T, bool>> _resultingExpression = null;

		foreach(var _specification in _specifications) {
			if(_resultingExpression is null) {
				_resultingExpression = _specification;
				continue;
			}

			_resultingExpression = Combine(_resultingExpression, _specification);
		}

		return _resultingExpression;
	}

	private static Expression<Func<T, bool>> Combine(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression) {
		var _parameter = Expression.Parameter(typeof(T));

		var _leftVisitor = new ReplaceExpressionVisitor(leftExpression.Parameters[0], _parameter);
		var _left = _leftVisitor.Visit(leftExpression.Body);

		var _rightVisitor = new ReplaceExpressionVisitor(rightExpression.Parameters[0], _parameter);
		var _right = _rightVisitor.Visit(rightExpression.Body);

		return Expression.Lambda<Func<T, bool>>(
			Expression.Or(_left, _right), _parameter);
	}

	private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
				: ExpressionVisitor
	{
		private readonly Expression _oldValue = oldValue;
		private readonly Expression _newValue = newValue;

		public override Expression Visit(Expression node) {
			return node == _oldValue ? _newValue : base.Visit(node);
		}
	}
}
