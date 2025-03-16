using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DTO;

internal sealed record InsurerDto (
    Guid Id,
    string Name,
    bool ActiveStatus,
    ICollection<NodeDto> Structure = null) {

    internal static ICollection<NodeDto> GetStructure(ICollection<Node> nodes) {
        return nodes is null 
            ? [] 
            : (from _node in nodes ?? [] select new NodeDto(
                _node.InsurerId, 
                _node.Parent is null ? null : new AgentDto(_node.Parent.Id, _node.Parent.Name, _node.Parent.ActiveStatus), 
                new AgentDto(_node.AgentId, _node.Agent.Name, _node.Agent.ActiveStatus), 
                _node.Left, 
                _node.Right, 
                GetStructure(_node.Nodes))).ToList();
    }
}
