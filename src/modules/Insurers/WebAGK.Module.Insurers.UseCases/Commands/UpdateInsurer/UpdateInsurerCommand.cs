using MediatR;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateInsurer;

public sealed record UpdateInsurerCommand(
    Guid Id,
    string Name,
    string Description) : IRequest {
    public ApiError Validate() {
        var _error = new ApiError();

        if (string.IsNullOrWhiteSpace(Name)) {
            _error.AddValidationError("Name", "name_is_required", "Name is required.");
        }
        
        return _error;
    }
}