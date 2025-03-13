using MediatR;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.UseCases.Commands.CreateAgent;

internal sealed record CreateAgentCommand(
    string LastName,
    string FirstName,
    string SecondName,
    string PersonalId,
    string TaxId,
    string RpuId,
    bool IsCompany
) : IRequest<Guid> {
    
    public ApiError Validate() {
        var _error = new ApiError();
        if (!Shared.Infrastructure.ValueObjects.PersonalId.IsValid(PersonalId)) {
            _error.AddValidationError("PersonalId", "invalid_personalid", "Invalid personal id.");
        }
        
        if (!Shared.Infrastructure.ValueObjects.TaxId.IsValid(TaxId)) {
            _error.AddValidationError("TaxlId", "invalid_taxid", "Invalid tax id.");
        }
                
        if (!Core.ValueObjects.RpuId.IsValid(RpuId)) {
            _error.AddValidationError("RpuId", "invalid_rpuid", "Invalid rpu id.");
        }
        
        if(IsCompany && TaxId is null) {
            _error.AddValidationError("TaxlId", "required_taxid", "Required tax id.");
        }

        if(string.IsNullOrWhiteSpace(PersonalId) && string.IsNullOrWhiteSpace(TaxId)) {
            _error.AddValidationError("Identifier", "required_identifier", "Required identifier.");
        }
        
        return _error;
    }
};