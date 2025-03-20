using WebAGK.Module.Insurers.Core.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DTO;

internal sealed record InsurerDto (
    Guid Id,
    string Name,
    bool ActiveStatus,
    ICollection<NodeDto> Structure = null) {

    internal static ICollection<NodeDto> GetStructure(ICollection<Node<Agent>> nodes) {
        nodes = nodes.OrderBy(x => x.Value.Name).ToList();
        return (from _node in nodes ?? [] select new NodeDto(
                new AgentDto(_node.Value.Id, _node.Value.Name, _node.Value.ActiveStatus), 
                _node.Left, 
                _node.Right, 
                GetStructure(_node.Nodes))).ToList();
    }
}
