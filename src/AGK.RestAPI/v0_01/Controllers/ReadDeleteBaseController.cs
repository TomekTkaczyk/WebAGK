using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

public abstract class ReadDeleteBaseController<TSelector,T>(ISender sender, ILogger<ApiBaseController> logger) 
	: ApiBaseController(sender, logger)	where TSelector : SelectorDTO where T : DTO
{
	//[HttpGet]
	//public virtual async Task<IActionResult> GetPageAsync(
	//	[FromQuery] GetPage<T> query,
	//	CancellationToken cancellationToken = default) {
	//	if(query.PageNumber < 1) {
	//		return BadRequest(new Error("pageNumber is invalid."));
	//	}

	//	if(query.PageNumber > 1 && query.PageSize == 0) {
	//		return BadRequest(new Error("pageNumber and/or pageSize is invalid."));
	//	}

	//	var response = await HandleRequestAsync<GetPage<T>, ApiResponse<Page<T>>>(query, cancellationToken);

	//	return response;
	//}


	//[HttpGet("{id:int}")]
	//public virtual async Task<IActionResult> GetAsync(
	// [FromRoute] int id,
	// CancellationToken cancellationToken = default) {
	//	var query = new GetById<T>(id);

	//	var response = await HandleRequestAsync<GetById<T>, ApiResponse<T>>(query, cancellationToken);

	//	return response;
	//}
}
