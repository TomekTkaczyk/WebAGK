using MediatR;
using WebAGK.Module.Insurers.Core.DTO;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurerStructure;

public record GetInsurerStructureQuery(Guid Id) : IRequest<InsurerDto>;