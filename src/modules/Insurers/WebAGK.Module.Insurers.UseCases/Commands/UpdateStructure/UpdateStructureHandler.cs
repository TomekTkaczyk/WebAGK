using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Abstractions.Messaging;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;

internal sealed class UpdateStructureHandler(
    IInsurerRepository insurerRepository,
    IStructureRepository structureRepository,
    INodeRepository nodeRepository,
    IAgentRepository agentRepository,
    IMessageBus messageBus,
    IInsurerUnitOfWork unitOfWork) : IRequestHandler<UpdateStructureCommand> {
    
    public async Task Handle(UpdateStructureCommand request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
           .Get(new ByIdSpecification<Insurer>(request.Id))
           .Include(x => x.Structure)
           .ThenInclude(x => x.Nodes.Where(n => n.ParentId == null))
           .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);

        _insurer.Structure.Clear();
        var _nodes = await GetStructure(_insurer.Structure, request.Structure);
        foreach (var _node in _nodes) {
            _insurer.Structure.AddNode(_node);
            nodeRepository.Add(_node);
        }
        _insurer.Structure.RenumberingStructure();
        insurerRepository.Update(_insurer);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<ICollection<Node>> GetStructure(Structure structure, ICollection<NodeDto> nodes, Node parent = null) {
        var _result = new List<Node>();
        
        foreach (var _nodeDto in nodes) {
            var _agent = await agentRepository.Get(new ByIdSpecification<Agent>(_nodeDto.Agent.Id)).SingleOrDefaultAsync();
            var _node = new Node(structure, _agent, parent);
            var _nodes = await GetStructure(structure, _nodeDto.Nodes, _node);
            foreach (var _child in _nodes) {
                _node.AddNode(_child);
            }
            _result.Add(_node);
        }
        
        return _result;
    }
}