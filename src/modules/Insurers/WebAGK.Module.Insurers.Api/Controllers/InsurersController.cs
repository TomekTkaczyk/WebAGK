using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Insurers.UseCases.Commands.CreateInsurer;
using WebAGK.Module.Insurers.UseCases.Commands.DeleteInsurer;
using WebAGK.Module.Insurers.UseCases.Commands.UpdateInsurer;
using WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurers;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Module.Insurers.Api.Controllers;

internal class InsurersController(IMediator mediator) : BaseController {

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] string searchText,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken = default) {
        
        var _query = new GetInsurersQuery(searchText, pageNumber, pageSize, isActive);

        return Ok(await mediator.Send(_query, cancellationToken));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInsurerAsync(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = default) {
		
        var _query = new GetInsurerQuery(id);
		
        return Ok(await mediator.Send(_query, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddInsurerAsync(
        [FromBody] CreateInsurerCommand command,
        CancellationToken cancellationToken = default) {

        var _error = command.Validate();
        if (_error.ValidationErrors.Any()) {
            _error.Message = "Bad request";
            _error.Status = StatusCodes.Status400BadRequest;
            throw new BadRequestException() {
                Error = _error,
            };           
        }
        
        await mediator.Send(command, cancellationToken);
	    
        return Created();
    }
    
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInsurerAsync(
        [FromRoute] Guid id, 
        [FromBody] UpdateInsurerCommand command, 
        CancellationToken cancellationToken = default) {

        if (!id.Equals(command.Id)) {
            return BadRequest();
        }
        
        var _error = command.Validate();
        if (_error.ValidationErrors.Any()) {
            _error.Message = "Bad request";
            _error.Status = StatusCodes.Status400BadRequest;
            throw new BadRequestException() {
                Error = _error,
            };           
        }
        
        await mediator.Send(command, cancellationToken);
	    
        return NoContent();
    }

    [HttpPut("{id:guid}/structure")]
    public async Task<IActionResult> UpdateStructureAsync(
        [FromRoute] Guid id, 
        [FromBody] UpdateStructureCommand command, 
        CancellationToken cancellationToken = default) {

        if (!id.Equals(command.Id)) {
            return BadRequest();
        }
        
        var _error = command.Validate();
        if (_error.ValidationErrors.Any()) {
            _error.Message = "Bad request";
            _error.Status = StatusCodes.Status400BadRequest;
            throw new BadRequestException() {
                Error = _error,
            };           
        }
        
        await mediator.Send(command, cancellationToken);
	    
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInsurerAsync(
        [FromRoute] Guid id, 
        CancellationToken cancellationToken = default) {
        
        await mediator.Send(new DeleteInsurerCommand(id), cancellationToken);

        return NoContent();
    }
}