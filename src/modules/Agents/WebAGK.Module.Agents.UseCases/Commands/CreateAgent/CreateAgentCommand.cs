using MediatR;

namespace WebAGK.Module.Agents.UseCases.Commands.CreateAgent;

internal sealed record CreateAgentCommand(
    string LastName,
    string FirstName,
    string SecondName,
    string PersonalId,
    string TaxId,
    bool IsActive,
    bool IsCompany
    ) : IRequest<Guid>;