using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurerStructure;

public class GetInsurerStructureHandler(
    IInsurerRepository repository) 
    : IRequestHandler<GetInsurerStructureQuery,InsurerDto> {
    public async Task<InsurerDto> Handle(GetInsurerStructureQuery request, CancellationToken cancellationToken) {
        var _insurer = await repository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .Include(x => x.Structure.OrderBy(o => o.Value.Name))
            .ThenInclude(x => x.Value)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        return InsurerDto.Create(_insurer);
    }
}

public class InsurerNotFoundException(Guid id) 
    : WebAgkException($"Insurer with ID: {id} is not exist.", StatusCodes.Status400BadRequest) {

    public Guid Id { get; } = id;
}