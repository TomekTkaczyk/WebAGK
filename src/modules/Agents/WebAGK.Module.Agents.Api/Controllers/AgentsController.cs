using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Agents.UseCases.Commands.CreateAgent;
using WebAGK.Module.Agents.UseCases.Queries.GetAgent;
using WebAGK.Module.Agents.UseCases.Queries.GetAgents;
using WebAGK.Shared.Abstractions.Contexts;
using WebAGK.Shared.Infrastructure.CQRS;

namespace WebAGK.Module.Agents.Api.Controllers;

[Route(AgentModule.BasePath + "/[controller]")]
internal class AgentsController(
    IMediator mediator) : HomeControllerBase {
	
    [HttpGet]
    public async Task<IActionResult> GetAsync(
	    [FromQuery] string searchText,
	    [FromQuery] int? pageNumber,
	    [FromQuery] int? pageSize,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken = default) {
	    
        var _query = new GetAgentsQuery(searchText, pageNumber, pageSize, isActive );
		
        return Ok(await mediator.Send(_query, cancellationToken));
    }
	
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAgentAsync(Guid id, CancellationToken cancellationToken) {
		
        var _query = new GetAgentQuery(id);
		
        return Ok(await mediator.Send(_query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> AddAgentAsync(CreateAgentCommand command,
	    CancellationToken cancellationToken = default) {

	    await mediator.Send(command, cancellationToken);
	    
	    return Created();
    }
}