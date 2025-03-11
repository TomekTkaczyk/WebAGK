using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace WebAGK.Shared.Infrastructure.Exceptions;
internal class ErrorHandlerMiddleware(
				IExceptionCompositionRoot exceptionCompositionRoot,
				ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
	private readonly IExceptionCompositionRoot _exceptionCompositionRoot = exceptionCompositionRoot;
	private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try {
			await next(context);
		}
		catch(Exception _exception) {
			_logger.LogError(_exception, "An error occurred: {Message}", _exception.Message);
			await HandleErrorAsync(context, _exception);
		}
	}

	private async Task HandleErrorAsync(HttpContext context, Exception exception)
	{

		var _errorResponse = _exceptionCompositionRoot.Map(exception);
		context.Response.StatusCode = (int)(_errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
		var _response = _errorResponse?.Response;

		if(_response is null) {
			return;
		}

		await context.Response.WriteAsJsonAsync(_response);
	}
}
