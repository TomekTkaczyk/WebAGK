using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Agents.UseCases.Queries.GetAgent;
using WebAGK.Module.Agents.UseCases.Queries.GetAgents;
using WebAGK.Shared.Infrastructure.CQRS;

namespace WebAGK.Module.Agents.Api.Controllers;

[Route(AgentModule.BasePath)]
[Authorize(Roles ="Admin")]
internal class HomeController(IMediator mediator) : HomeControllerBase
{
	[HttpGet]
	public async Task<IActionResult> GetAsync(GetPageRequest getPageRequest, CancellationToken cancellationToken = default) {
		
		var query = new GetAgentsQuery(getPageRequest.SearchText, getPageRequest.PageNumber, getPageRequest.PageSize, getPageRequest.IsActive );
		
		return Ok(await mediator.Send(query, cancellationToken));
	}
	
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetAgentAsync(Guid id, CancellationToken cancellationToken) {
		
		var query = new GetAgentQuery(id);
		
		return Ok(await mediator.Send(query, cancellationToken));
	}
}
