using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Module.Agents.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Infrastructure.ValueObjects;

namespace WebAGK.Module.Agents.UseCases.Commands.CreateAgent;

internal sealed class CreateAgentHandler(
    IAgentRepository repository,
    IAgentUnitOfWork unitOfWork)
    : IRequestHandler<CreateAgentCommand, Guid>
{

    public async Task<Guid> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
    {
        var _error = new ApiError();
        Agent _agent;
        if (request.TaxId is not null) {
            try {
                TaxId _taxId = request.TaxId;
                _agent = await repository
                    .Get(new GetByTaxIdSpecification(_taxId))
                    .SingleOrDefaultAsync(cancellationToken);
                if(_agent is not null) {
                    _error.AddValidationError("TaxId", "taxid_already_exist", "Tax id already exists.");
                }
            }
            catch (WebAGKException _ex) {
                _error.Message = _ex.Message;
                _error.AddValidationError("TaxId", "invalid_taxid", "Invalid tax id.");
            }    
        }

        if (request.PersonalId is not null) {
            try {
                PersonalId _personalId = request.PersonalId;
                _agent = await repository
                    .Get(new GetByPersonalIdSpecification(_personalId))
                    .SingleOrDefaultAsync(cancellationToken);
                if(_agent is not null) {
                    _error.AddValidationError("PersonalId", "personalid_already_exist", "Personal id already exists.");
                }        }
            catch (WebAGKException _ex) {
                _error.Message = _ex.Message;
                _error.AddValidationError("PersonalId", "invalid_personalid", "Invalid Personal id.");
            }
        }
        
        if(_error.ValidationErrors.Any()) {

            throw new InvalidIdentifierException()
            {
                Error = _error
            };
        }

        _agent = Agent.Create(
            lastName: request.LastName,
            firstName: request.FirstName,
            secondName: request.SecondName,
            personalId: request.PersonalId,
            taxId: request.TaxId,
            isCompany: request.IsCompany);
        
        _agent.Validate(); 

        repository.Add(_agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return _agent.Id;
    }
}