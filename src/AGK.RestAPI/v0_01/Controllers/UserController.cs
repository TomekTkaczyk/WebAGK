using AGK.Application.Dto;
using AGK.Application.Features.UserManagment.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AGK.RestAPI.v0_01.Controllers;

// [Authorize]
[ApiController]
[Route("api/v0_01/users")]
public class UserController : ActiveStatusController<UserDTO>
{
	public UserController(
		ISender sender,
		ILogger<ApiBaseController> logger) : base(sender, logger) { }


	[HttpPatch("{id:int}")]
	public virtual async Task<IActionResult> UpdateAsync(
		int id,
		[FromBody] UpdateUser command,
		CancellationToken cancellationToken = default) {
		if(id != command.Id) {
			return BadRequest();
		}

		return await HandleRequestAsync(command, cancellationToken);

	}
}
