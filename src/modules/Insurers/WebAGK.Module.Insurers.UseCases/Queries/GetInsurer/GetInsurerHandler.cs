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
    IInsurerRepository insurerRepository,
    INodeRepository nodeRepository) 
    : IRequestHandler<GetInsurerQuery,InsurerDto> {
    public async Task<InsurerDto> Handle(GetInsurerQuery request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
            .Get(new ByIdSpecification<Insurer>(request.Id))
            .Include(i => i.Structure)
            .ThenInclude(x => x.Nodes.Where(n => n.ParentId == null))
            .ThenInclude(x => x.Agent)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);

        var _nodes = await nodeRepository
            .Get()
            .Include(x => x.Agent)
            .Where(x => x.StructureId == _insurer.Structure.Id && x.ParentId != null)
            .OrderBy(x => x.Left)
            .ToListAsync(cancellationToken);
            
        var _nodeDict = _nodes.ToDictionary(n => n.Id);
        
        foreach (var _node in _nodes)
        {
            if (_node.ParentId == null)
            {
                _insurer.Structure.AddNode(_node);
            }
            else if (_nodeDict.TryGetValue(_node.ParentId.Value, out var _parent))
            {
                _parent.AddNode(_node);
            }
        }
        
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