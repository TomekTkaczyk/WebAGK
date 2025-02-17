using Humanizer;

namespace WebAGK.Shared.Abstractions.Exceptions;

public abstract class WebAGKException : Exception
{
	private string ErrorCode => GetType().Name.Underscore().Replace("_exception", string.Empty).ToLowerInvariant();

	public ApiError Error { get; init; }

	public WebAGKException(string message, int status) : base(message)
	{
		Error = new ApiError()
		{
			Message = message,
			Code = ErrorCode,
			Status = status
		};
	}
}
