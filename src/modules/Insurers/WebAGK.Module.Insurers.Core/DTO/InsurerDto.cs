using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DTO;

public sealed class InsurerDto {
    
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool ActiveStatus {get; init;}
    public ICollection<NodeDto> Nodes { get; init; }
    
    private InsurerDto() { }

    public static InsurerDto Create(Insurer insurer) {
        return new InsurerDto() {
            Id = insurer.Id,
            Name = insurer.Name,
            ActiveStatus = insurer.ActiveStatus,
            Nodes = GetNodes(insurer.Structure)
        };
    }

    private static ICollection<NodeDto> GetNodes(ICollection<Node> nodes) {
        return nodes.Select(x 
            => new NodeDto(
                x.Id, 
                x.ParentId,
                new AgentDto(x.Value.Id, x.Value.Name, x.Value.ActiveStatus), 
                x.Left, 
                x.Right,
                GetNodes(x.Nodes))).ToList();
    }
}