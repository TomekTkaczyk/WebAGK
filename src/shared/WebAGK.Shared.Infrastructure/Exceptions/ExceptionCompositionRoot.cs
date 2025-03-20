using Microsoft.Extensions.DependencyInjection;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;
internal class ExceptionCompositionRoot(IServiceProvider serviceProvider) : IExceptionCompositionRoot
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public ExceptionResponse Map(Exception exception) {
		using(var _scope = _serviceProvider.CreateScope()) {
			var _mappers = _scope.ServiceProvider.GetServices<IExceptionToResponseMapper>();
			var _nonDefaultMappers = _mappers.Where(x => x is not ExceptionToResponseMapper).ToArray();
			var _result = _nonDefaultMappers
				.Select(x => x.Map(exception))
				.SingleOrDefault(x => x is not null);

			if(_result is not null) {
				return _result;
			}

			var _defaultMapper = _mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);

			return _defaultMapper?.Map(exception);
		}
	}
}
