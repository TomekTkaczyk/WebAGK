using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DTO;

internal sealed record InsurerDto (
    Guid Id,
    string Name,
    bool ActiveStatus,
    ICollection<NodeDto> Structure = null) {

    internal static ICollection<NodeDto> GetStructure(IReadOnlyCollection<Node> nodes) {
        nodes = nodes.OrderBy(x => x.Agent.Name).ToList();
        return (from _node in nodes ?? [] select new NodeDto(
                new AgentDto(_node.Agent.Id, _node.Agent.Name, _node.Agent.ActiveStatus), 
                _node.Left, 
                _node.Right, 
                GetStructure(_node.Nodes))).ToList();
    }
}
