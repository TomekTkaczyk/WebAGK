using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAGK.Shared.Abstractions.Exceptions;
using System.Net;

namespace WebAGK.Shared.Infrastructure.Contexts;
internal class VaidateModelAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		if(context.ModelState.IsValid) {
			return;
		}

		var apiError = new ApiError
		{
			Code = "validation_error",
			Status = StatusCodes.Status400BadRequest,
			Message = "Request validation failed."
		};

		var validationErrors = context.ModelState
			.Where(ms => ms.Value.Errors.Count > 0)
			.SelectMany(ms => ms.Value.Errors.Select(e => new ValidationError
			(
				ms.Key,
				ms.Key.Underscore().ToLowerInvariant() + "_validation_error",
				e.ErrorMessage
			)))
			.ToList();

		foreach(var validationError in validationErrors) {
			apiError.AddValidationError(validationError.Field, validationError.Code, validationError.Message);
		}

		context.Result = new BadRequestObjectResult(apiError);
	}
}
