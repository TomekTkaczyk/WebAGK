using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Infrastructure.Entities;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;

internal sealed class UpdateStructureHandler(
    IInsurerRepository insurerRepository,
    IAgentRepository agentRepository,
    IInsurerUnitOfWork unitOfWork) : IRequestHandler<UpdateStructureCommand> {
    
    public async Task Handle(UpdateStructureCommand request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
           .Get(new ByIdSpecification<Insurer>(request.Id))
           .Include(x => x.Structure)
           .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);

        _insurer.Structure.ClearStructure();
        var _nodes = await GetStructure(request.Structure);
        foreach (var _node in _nodes) {
            _insurer.Structure.AddNode(_node);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<ICollection<Node<Agent>>> GetStructure(ICollection<NodeDto> nodes, Node<Agent> parent = null) {
        var _result = new List<Node<Agent>>();
        
        foreach (var _nodeDto in nodes) {
            var _agent = await agentRepository.Get(new ByIdSpecification<Agent>(_nodeDto.Agent.Id)).SingleOrDefaultAsync();
            var _node = new Node<Agent>(_agent, parent);
            var _nodes = await GetStructure(_nodeDto.Nodes, _node);
            foreach (var _child in _nodes) {
                _node.Nodes.Add(_child);
            }
            _result.Add(_node);
        }
        
        return _result;
    }
}