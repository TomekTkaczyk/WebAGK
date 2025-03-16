using MediatR;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;

public sealed record UpdateStructureCommand(
    Guid Id,
    ICollection<NodeDto> Structure) : IRequest {
    public ApiError Validate() {
        var _error = new ApiError();
        
        
        return _error;
    }
};
