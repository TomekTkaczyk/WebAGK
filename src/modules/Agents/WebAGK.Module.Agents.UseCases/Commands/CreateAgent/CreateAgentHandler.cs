using MediatR;
using WebAGK.Module.Agents.Core.Entities;
using WebAGK.Module.Agents.Core.Exceptions;
using WebAGK.Module.Agents.Core.Repositories;
using WebAGK.Shared.Abstractions.Exceptions;

namespace WebAGK.Module.Agents.UseCases.Commands.CreateAgent;

internal sealed class CreateAgentHandler(IAgentRepository repository) : IRequestHandler<CreateAgentCommand,Guid> {
    public async Task<Guid> Handle(CreateAgentCommand request, CancellationToken cancellationToken) {

        var error = new ApiError();
        
        var agent = await repository.GetByTaxIdAsync(request.TaxId, cancellationToken);
        if (agent is not null) {
            error.AddValidationError("TaxId", "taxid_already_exist", "Tax id already exists.");
        }
        
        agent = await repository.GetByPersonalIdAsync(request.PersonalId, cancellationToken);
        if (agent is not null) {
            error.AddValidationError("PersonalId", "personalid_already_exist", "Personal id already exists.");
        }

        if(error.ValidationErrors.Any()) {
            throw new InvalidIdentifierException()
            {
                Error = error
            };
        }

        agent = Agent.Create(
            lastName: request.LastName,
            firstName: request.FirstName,
            secondName: request.SecondName,
            personalId: request.PersonalId,
            taxId: request.TaxId,
            isCompany: request.IsCompany);
        
        return agent.Id;
    }
}