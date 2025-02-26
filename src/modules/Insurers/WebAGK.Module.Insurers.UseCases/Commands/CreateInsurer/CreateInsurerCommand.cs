using MediatR;

namespace WebAGK.Module.Insurers.UseCases.Commands.CreateInsurer;

public record CreateInsurerCommand(string Name) : IRequest<Guid>;