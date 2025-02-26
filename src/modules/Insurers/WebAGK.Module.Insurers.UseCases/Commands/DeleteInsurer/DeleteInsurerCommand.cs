using MediatR;

namespace WebAGK.Module.Insurers.UseCases.Commands.DeleteInsurer;

public sealed record DeleteInsurerCommand(Guid Id) : IRequest;