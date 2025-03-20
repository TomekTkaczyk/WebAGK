using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Shared.Infrastructure.Exceptions;
public interface IExceptionCompositionRoot
{
	ExceptionResponse Map(Exception exception);
}
