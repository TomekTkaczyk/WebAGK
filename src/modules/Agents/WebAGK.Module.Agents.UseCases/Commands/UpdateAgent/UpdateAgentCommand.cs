using MediatR;

namespace WebAGK.Module.Agents.UseCases.Commands.UpdateAgent;

public sealed record UpdateAgentCommand(
    Guid Id,
    string LastName,
    string FirstName,
    string SecondName,
    string PersonalId,
    string TaxId,
    bool IsActive,
    bool IsCompany,
    string Description
) : IRequest;