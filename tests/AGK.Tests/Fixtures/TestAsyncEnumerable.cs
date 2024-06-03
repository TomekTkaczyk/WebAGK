using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Tests.Fixtures;

internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{

	public TestAsyncEnumerable(IEnumerable<T> enumerable)
	: base(enumerable) { }

	public TestAsyncEnumerable(Expression expression)
		: base(expression) { }

	public IAsyncEnumerator<T> GetEnumerator() {
		return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
	}

	public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) {
		throw new NotImplementedException();
	}

	IQueryProvider IQueryable.Provider {
		get { return new TestAsyncQueryProvider<T>(this); }
	}

}
