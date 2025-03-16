using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Module.Insurers.Core.Repositories;
using WebAGK.Module.Insurers.UseCases.Queries.GetInsurer;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Insurers.UseCases.Commands.UpdateStructure;

public sealed class UpdateStructureHandler(
    IInsurerRepository repository,
    IInsurerUnitOfWork unitOfWork) : IRequestHandler<UpdateStructureCommand> {
    public async Task Handle(UpdateStructureCommand request, CancellationToken cancellationToken) {
        var _insurer = await repository
           .Get(new ByIdSpecification<Insurer>(request.Id))
           .Include(x => x.Structure)
           .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InsurerNotFoundException(request.Id);
        
        _insurer.Structure = GetNodes(request.Structure);
        _insurer.RenumberingStructure();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ICollection<Node> GetNodes(ICollection<NodeDto> nodes) {
        var _result  = new List<Node>();
        foreach (var _node in nodes) {
            _result.Add(new Node() {
                InsurerId = _node.InsurerId,
                AgentId = _node.Agent is null ? Guid.Empty : _node.Agent.Id,
                Agent = _node.Agent is null ? null : Agent.Create(_node.Agent),
                Parent = _node.Parent is null ? null : Agent.Create(_node.Parent),
                Nodes = GetNodes(_node.Nodes)
            });
        }
        
        return _result;
    }
}