using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;

public sealed class UpdateStructureHandler(
    IInsurerRepository insurerRepository,
    IAgentRepository agentRepository,
    INodeRepository nodeRepository,
    IInsurerUnitOfWork unitOfWork) : IRequestHandler<UpdateStructureCommand> {
    public async Task Handle(UpdateStructureCommand request, CancellationToken cancellationToken) {
        var _insurer = await insurerRepository
           .Get(new ByIdSpecification<Insurer>(request.Id))
           .Include(x => x.Structure)
           .ThenInclude(x => x.Agent)
           .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        _insurer.Structure = await GetNodes(request.Structure, _insurer.Id);
        _insurer.RenumberingStructure();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<ICollection<Node>> GetNodes(ICollection<NodeDto> nodes, Guid insurerId) {
        var _result  = new List<Node>();
        foreach (var _node in nodes) {
            var _agent = await agentRepository.Get(new ByIdSpecification<Agent>(_node.Agent.Id)).SingleOrDefaultAsync();
            _result.Add(new Node() {
                InsurerId = insurerId,
                AgentId = _agent.Id,
                Agent = _agent,
                Nodes = await GetNodes(_node.Nodes, insurerId)
            });
        }
        
        return _result;
    }
}