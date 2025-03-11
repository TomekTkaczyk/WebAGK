using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Specifications;
using WebAGK.Shared.Infrastructure.CQRS;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurers;

internal class GetInsurersHandler(
    IInsurerRepository repository)
    : IRequestHandler<GetInsurersQuery, Page<InsurerDto>> {
    public async Task<Page<InsurerDto>> Handle(GetInsurersQuery request, CancellationToken cancellationToken) {
        var _query = repository.Get();
        var _total = await _query.CountAsync(cancellationToken);
        var _agents = _query
            .OrderBy(x => x.Name)
            .Skip(request.PageSize * (request.PageNumber - 1));
        if (request.PageSize > 0) {
            _agents = _agents.Take(request.PageSize);
        }
        var _collection = await _agents
            .Select(x => InsurerDto.Create(x))
            .ToListAsync(cancellationToken);      
        
        return Page<InsurerDto>.Create(request.PageNumber, request.PageSize, _total, _collection);
    }
}