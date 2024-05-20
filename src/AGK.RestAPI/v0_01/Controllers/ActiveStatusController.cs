using AGK.Application.Features.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

public abstract class ActiveStatusController<T> : ReadDeleteBaseController<T>
{
	protected ActiveStatusController(
		ISender sender, 
		ILogger<ApiBaseController> logger) : base(sender, logger) {	}

	[HttpPatch("{id:int}/status/{status:int}")]
	public async Task<IActionResult> SetActiveStatusAsync(
		[FromRoute] int id,
		[FromRoute] int status,
		CancellationToken cancellationToken = default) {
		if(status > 1 && status < 0) {
			return BadRequest();
		}

		var command = new SetActive<T>(id, status == 1);

		return await HandleRequestAsync(command, cancellationToken);
	}
}
