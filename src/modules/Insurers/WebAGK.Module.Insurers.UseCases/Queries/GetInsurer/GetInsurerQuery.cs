using MediatR;
using WebAGK.Module.Insurers.Core.DTO;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;

public record GetInsurerQuery(Guid Id) : IRequest<InsurerDto>;