using AGK.Application.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

public abstract class ApiBaseController(ISender sender, ILogger<ApiBaseController> logger) : ControllerBase
{
	protected readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
	protected readonly ILogger<ApiBaseController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

	protected async Task<IActionResult> HandleRequestAsync<TRequest>(
		TRequest request,
		CancellationToken cancellationToken = default) where TRequest : IRequest {
		if(!ModelState.IsValid) {
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors }));
		}

		await _sender.Send(request, cancellationToken);

		return NoContent();
	}

	protected async Task<IActionResult> HandleRequestAsync<TRequest, TResponse>(
		TRequest request,
		CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse> where TResponse : ApiResponse {
		if(!ModelState.IsValid) {
			return BadRequest(ModelState
				.Where(x => x.Value.Errors.Any())
				.Select(x => new { property = x.Key, errors = x.Value.Errors }));
		}

		var response = await _sender.Send(request, cancellationToken);

		return StatusCode((int)response.HttpCode, response);
	}
}