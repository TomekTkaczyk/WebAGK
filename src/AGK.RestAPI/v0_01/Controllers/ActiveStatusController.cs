using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

public abstract class ActiveStatusController<TSelector,T>(ISender sender, ILogger<ApiBaseController> logger) 
	: ReadDeleteBaseController<TSelector,T>(sender, logger) where TSelector : SelectorDTO where T : DTO
{
	[HttpPatch("{id:int}/status/{status:int}")]
	public async Task<IActionResult> SetActiveStatusAsync(
		[FromRoute] int id,
		[FromRoute] int status,
		CancellationToken cancellationToken = default) {
		if(status > 1 && status < 0) {
			return BadRequest();
		}

		var command = new SetActive<TSelector>(id, status == 1);

		return await HandleRequestAsync<SetActive<TSelector>,ApiResponse>(command, cancellationToken);
	}
}
