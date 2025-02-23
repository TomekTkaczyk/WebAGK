using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Agents.Core.DAL.Repositories;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Module.Agents.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.UseCases.Commands.CreateAgent;

internal sealed class CreateAgentHandler(
    IAgentRepository repository, 
    IAgentUnitOfWork unitOfWork)
    : IRequestHandler<CreateAgentCommand,Guid> {
    
    public async Task<Guid> Handle(CreateAgentCommand request, CancellationToken cancellationToken) {

        var _error = new ApiError();
        var _agent = await repository
            .Get(new GetByTaxIdSpecification(request.TaxId))
            .SingleOrDefaultAsync(cancellationToken);
        if (_agent is not null) {
            _error.AddValidationError("TaxId", "taxid_already_exist", "Tax id already exists.");
        }
        
        _agent = await repository
            .Get(new GetByPersonalIdSpecification(request.PersonalId))
            .SingleOrDefaultAsync(cancellationToken);
        if (_agent is not null) {
            _error.AddValidationError("PersonalId", "personalid_already_exist", "Personal id already exists.");
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
        
        repository.Add(_agent);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _agent.Id;
    }
}