using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Tests.Fixtures;
internal class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T> {

	private readonly IEnumerator<T> _inner = inner;

	public T Current { get {return _inner.Current;} }

	public ValueTask DisposeAsync() {
		throw new NotImplementedException();
	}

	public ValueTask<bool> MoveNextAsync() {
		throw new NotImplementedException();
	}

	public void Dispose() {
		_inner.Dispose();
	}

	public Task<bool> MoveNext() {
		return Task.FromResult(_inner.MoveNext());
	}
}
