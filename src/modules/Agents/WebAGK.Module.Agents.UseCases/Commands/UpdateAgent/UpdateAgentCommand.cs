using MediatR;

namespace WebAGK.Module.Agents.UseCases.Commands.UpdateAgent;

internal sealed record UpdateAgentCommand(
    Guid Id,
    string LastName,
    string FirstName,
    string SecondName,
    string PersonalId,
    string TaxId,
    bool ActiveStatus,
    bool IsCompany,
    string Description
) : IRequest;