using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Features.UserManagment.Commands;
using AGK.Application.Reponse;
using AGK.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/users")]
public class UserController( ISender sender, ILogger<ApiBaseController> logger) 
	: ActiveStatusController<UserSelectorDTO,DTO>(sender, logger)
{
	[HttpGet]
	public virtual async Task<IActionResult> GetUsers(
		[FromQuery] string searchString,
		[FromQuery] bool? activeStatus,
		[FromQuery] int? pageNumber,
		[FromQuery] int? pageSize,
		CancellationToken cancellationToken = default) {

		var query = new GetPage<UserDTO>(searchString, activeStatus, pageNumber, pageSize);

		try {
			var response = await HandleRequestAsync<GetPage<UserDTO>, ApiResponse<Page<UserDTO>>>(query, cancellationToken);

			return Ok(response);
		}
		catch(Exception ex) {
			return BadRequest(ex.Message);
		}
	}


	//[HttpGet("{id:TypeEntityId}")]
	//public virtual async Task<IActionResult> GetUser(
	//	[FromRoute] TypeEntityId Id,
	//	CancellationToken cancellationToken = default) {

	//	var response = await HandleRequestAsync<GetById<UserDTO>, ApiResponse<UserDTO>>(
	//		new GetById<UserDTO>(Id),
	//		cancellationToken);

	//	return Ok(response);
	//}

	//[HttpPost]
	//public virtual async Task<IActionResult> SignUp(
	//	[FromBody] SignUp command,
	//	CancellationToken cancellationToken = default) {

	//	var response = await HandleRequestAsync<SignUp, ApiResponse<EntityId>>(command, cancellationToken);

	//	return Ok();
	//}
}
