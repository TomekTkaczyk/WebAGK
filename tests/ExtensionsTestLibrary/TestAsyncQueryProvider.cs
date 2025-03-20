using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace ExtensionsTests;

public class TestAsyncQueryProvider<TEntity>(IQueryProvider innerQueryProvider) : IAsyncQueryProvider {

	public IQueryable CreateQuery(Expression expression)
		=> new TestAsyncEnumerable<TEntity>(expression);

	public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		=> new TestAsyncEnumerable<TElement>(expression);

	public object Execute(Expression expression)
		=> innerQueryProvider.Execute(expression);

	public TResult Execute<TResult>(Expression expression)
		=> innerQueryProvider.Execute<TResult>(expression);

	public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
		=> Task.FromResult(innerQueryProvider.Execute<TResult>(expression)).GetAwaiter().GetResult();

}
