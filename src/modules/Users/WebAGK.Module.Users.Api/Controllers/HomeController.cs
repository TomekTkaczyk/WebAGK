using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Users.UseCases.Commands.UpdatePermissions;
using WebAGK.Module.Users.UseCases.Queries.GetUser;
using WebAGK.Module.Users.UseCases.Queries.GetUsers;

namespace WebAGK.Module.Users.Api.Controllers;

[Route(UserModule.BasePath)]
[Authorize(Policy ="Users.UserManagerOrAdmin")]
internal class HomeController(IMediator mediator) : HomeControllerBase
{
	private readonly IMediator mediator = mediator;

	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		return Ok(await mediator.Send(new GetUsersQuery(), cancellationToken));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
	{
		return Ok(await mediator.Send(new GetUserQuery(id), cancellationToken));
	}

	[HttpPost("update-permissions")]
	public async Task<IActionResult> UpdatePermissions([FromBody] UpdatePermissionsCommand command, CancellationToken cancellationToken)
	{
		await mediator.Send(command, cancellationToken);

		return NoContent();
	}
}
