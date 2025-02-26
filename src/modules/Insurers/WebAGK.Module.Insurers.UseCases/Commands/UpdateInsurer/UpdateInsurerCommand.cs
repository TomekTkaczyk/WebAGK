using MediatR;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateInsurer;

public sealed record UpdateInsurerCommand(
    Guid Id,
    string Name,
    string Description) : IRequest;