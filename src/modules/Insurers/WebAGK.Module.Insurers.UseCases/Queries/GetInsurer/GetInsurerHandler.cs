using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Shared.Abstractions.Exceptions;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;

internal class GetInsurerHandler(
    IInsurerRepository insurerRepository) 
    : IRequestHandler<GetInsurerQuery,InsurerDto> {
    public async Task<InsurerDto> Handle(GetInsurerQuery request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .Include(i => i.Structure)
            .ThenInclude(x => x.Nodes)
            .ThenInclude(x => x.Agent)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);

        
        return new InsurerDto(
            _insurer.Id, 
            _insurer.Name, 
            _insurer.ActiveStatus,
            InsurerDto.GetStructure(_insurer.Structure.Nodes));
    }
}

public class InsurerNotFoundException(Guid id) 
    : WebAgkException($"Insurer with ID: {id} is not exist.", StatusCodes.Status400BadRequest) {

    public Guid Id { get; } = id;
}