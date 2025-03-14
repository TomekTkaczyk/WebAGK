using MediatR;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Insurers.UseCases.Commands.CreateInsurer;

public record CreateInsurerCommand(
    string Name) : IRequest<Guid> {

    public ApiError Validate() {
        var _error = new ApiError();

        if (string.IsNullOrWhiteSpace(Name)) {
            _error.AddValidationError("Name", "name_is_required", "Name is required.");
        }
        
        return _error;
    }
}