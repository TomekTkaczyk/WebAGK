using WebAGK.Shared.Abstractions.Exceptions;
using System.Net;

namespace WebAGK.Shared.Infrastructure.Exceptions;
internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
	public ExceptionResponse Map(Exception exception)
	{

		switch(exception) {
			case WebAgkException _ex:
				_ex.Error.Message = _ex.Message;
				_ex.Error.Status = (int)GetStatusCode(_ex.Error.Status);
				return new ExceptionResponse(_ex.Error, GetStatusCode(_ex.Error.Status));

			default:
				return new ExceptionResponse(new ApiError()
				{
					Code = "internal_server_error",
					Message = "There was an error.",
					Status = (int)HttpStatusCode.InternalServerError
				}, HttpStatusCode.InternalServerError);
		}
	}

	private record ErrorsResponse(ApiError Error);

	private static HttpStatusCode GetStatusCode(int status)
		=> status switch
		{
			401 => HttpStatusCode.Unauthorized,
			_ => HttpStatusCode.BadRequest
		};

}
