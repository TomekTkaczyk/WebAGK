using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

public abstract class ReadDeleteBaseController<T>(ISender sender, ILogger<ApiBaseController> logger) 
	: ApiBaseController(sender, logger)
{
	[HttpGet]
	public virtual async Task<IActionResult> GetPageAsync(
		[FromQuery] GetPage<T> query,
		CancellationToken cancellationToken = default) {
		if(query.PageNumber < 1) {
			return BadRequest(new Error("pageNumber is invalid."));
		}

		if(query.PageNumber > 1 && query.PageSize == 0) {
			return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
		}

		return await HandleRequestAsync<GetPage<T>, ApiResponse<Page<T>>>(query, cancellationToken);
	}


	[HttpGet("{id:int}")]
	public virtual async Task<IActionResult> GetAsync(
	 [FromRoute] int id,
	 CancellationToken cancellationToken = default) {
		var query = new GetById<T>(id);

		return await HandleRequestAsync<GetById<T>, ApiResponse<T>>(query, cancellationToken);
	}


	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteAsync(
		[FromRoute] int id,
		CancellationToken cancellationToken = default) {
		var command = new Delete<T>(id);

		return await HandleRequestAsync(command, cancellationToken);
	}
}
