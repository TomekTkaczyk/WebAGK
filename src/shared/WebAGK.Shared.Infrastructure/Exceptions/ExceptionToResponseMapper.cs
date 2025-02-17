using WebAGK.Shared.Abstractions.Exceptions;
using System.Net;

namespace WebAGK.Shared.Infrastructure.Exceptions;
internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
	public ExceptionResponse Map(Exception exception)
		=> exception switch {
			WebAGKException ex => new ExceptionResponse(ex.Error, GetStatusCode(ex.Error.Status)), 
			_ => new ExceptionResponse(new ApiError()
			{
				Code = "internal_server_error",
				Message = "There was an error."
			}, HttpStatusCode.InternalServerError)
		};

	private record ErrorsResponse(ApiError Error);

	private static HttpStatusCode GetStatusCode(int status)
		=> status switch
		{
			401 => HttpStatusCode.Unauthorized,
			_ => HttpStatusCode.BadRequest
		};

}
