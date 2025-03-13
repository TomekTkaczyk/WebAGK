using WebAGK.Shared.Abstractions.Exceptions;
using System.Net;

namespace WebAGK.Shared.Infrastructure.Exceptions;
internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
	public ExceptionResponse Map(Exception exception)
	{

		switch(exception) {
			case WebAgkException ex:
				ex.Error.Message = ex.Message;
				ex.Error.Status = (int)GetStatusCode(ex.Error.Status);
				return new ExceptionResponse(ex.Error, GetStatusCode(ex.Error.Status));

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
