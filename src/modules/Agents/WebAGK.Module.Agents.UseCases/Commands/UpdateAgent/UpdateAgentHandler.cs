using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Module.Agents.Core.Validators;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Agents.UseCases.Commands.UpdateAgent;

internal sealed class UpdateAgentHandler(
    IAgentRepository repository,
    IAgentUnitOfWork unitOfWork) : IRequestHandler<UpdateAgentCommand> {
    public async Task Handle(UpdateAgentCommand request, CancellationToken cancellationToken) {
        var _error = new ApiError();
        var _agent = await repository
            .Get(new ByIdSpecification<Agent>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new AgentNotFoundException(request.Id);
        
        _agent.LastName = request.LastName;
        _agent.FirstName = request.FirstName;
        _agent.SecondName = request.SecondName;
        try {
            _agent.PersonalId = request.PersonalId;
        }
        catch (WebAgkException _ex) {
            _error.Message = _ex.Message;
            _error.AddValidationError("PersonalId", "invalid_personalid", "Invalid Personal id.");
        }
        try {
            _agent.TaxId = request.TaxId;
        }
        catch (WebAgkException _ex) {
            _error.Message = _ex.Message;
            _error.AddValidationError("TaxId", "invalid_taxid", "Invalid Tax id.");
        }
        _agent.ActiveStatus = request.ActiveStatus;
        _agent.IsCompany  = request.IsCompany;
        _agent.Description = request.Description;

        if(_error.ValidationErrors.Any()) {

            throw new InvalidIdentifierException()
            {
                Error = _error
            };
        }
        
        _agent.Validate();

        var _otherAgent = await repository
            .Get()
            .Where(a => !a.Id.Equals(_agent.Id) && (a.PersonalId.Equals(_agent.PersonalId) || a.TaxId.Equals(_agent.TaxId)))
            .FirstOrDefaultAsync(cancellationToken);

        if (_otherAgent is not null) {
            throw new IdentifierConflictException();
        }
        
        repository.Update(_agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
