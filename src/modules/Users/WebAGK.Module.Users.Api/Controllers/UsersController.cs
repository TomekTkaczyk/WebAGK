using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Users.UseCases.Commands.DeleteUser;
using WebAGK.Module.Users.UseCases.Commands.UpdatePermissions;
using WebAGK.Module.Users.UseCases.Queries.GetUser;
using WebAGK.Module.Users.UseCases.Queries.GetUsers;

namespace WebAGK.Module.Users.Api.Controllers;

[Authorize(Policy ="Users.UserManagerOrAdmin")]
internal class UsersController(IMediator mediator) : BaseController
{
	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		return Ok(await mediator.Send(new GetUsersQuery(), cancellationToken));
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
	{
		return Ok(await mediator.Send(new GetUserQuery(id), cancellationToken));
	}

	[HttpPut("update-permissions")]
	public async Task<IActionResult> UpdatePermissions([FromBody] UpdatePermissionsCommand command, CancellationToken cancellationToken)
	{
		await mediator.Send(command, cancellationToken);

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) {
		await mediator.Send(new DeleteUserCommand(id), cancellationToken);
		
		return NoContent();
	}

}
