using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Agents.UseCases.Commands.CreateAgent;
using WebAGK.Module.Agents.UseCases.Commands.DeleteAgent;
using WebAGK.Module.Agents.UseCases.Commands.UpdateAgent;
using WebAGK.Module.Agents.UseCases.Queries.GetAgent;
using WebAGK.Module.Agents.UseCases.Queries.GetAgents;

namespace WebAGK.Module.Agents.Api.Controllers;

internal class AgentsController(IMediator mediator) : BaseController {
	
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
    public async Task<IActionResult> GetAgentAsync(
	    [FromQuery] Guid id, 
	    CancellationToken cancellationToken = default) {
		
        var _query = new GetAgentQuery(id);
		
        return Ok(await mediator.Send(_query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> AddAgentAsync(
	    [FromBody] CreateAgentCommand command,
	    CancellationToken cancellationToken = default) {

	    await mediator.Send(command, cancellationToken);
	    
	    return Created();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAgentAsync(
	    [FromQuery] Guid id, 
	    [FromBody] UpdateAgentCommand command, 
	    CancellationToken cancellationToken = default) {

	    if (id.Equals(command.Id)) {
		    return BadRequest();
	    }
	    await mediator.Send(command, cancellationToken);
	    
	    return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAgentAsync(
	    [FromQuery] Guid id, 
	    CancellationToken cancellationToken = default) {
	    
	    await mediator.Send(new DeleteAgentCommand(id), cancellationToken);
	    
	    return NoContent();
    }
}