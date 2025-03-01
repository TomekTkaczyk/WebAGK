using MediatR;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Shared.Infrastructure.CQRS;
using WebAGK.Shared.Infrastructure.ValueObjects;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurers;

internal sealed record GetInsurersQuery(string SearchText, PageNumber PageNumber, PageSize PageSize, bool? IsActive ) 
    : IRequest<Page<InsurerDto>>;
    