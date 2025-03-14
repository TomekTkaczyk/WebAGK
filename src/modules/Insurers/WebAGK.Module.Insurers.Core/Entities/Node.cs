using WebAGK.Shared.Infrastructure.Types;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Node : Node<Agent> {
    public Guid Id { get; set; }
    public Guid ValueId { get; set; }
    public Guid? ParentId { get; set; }
    public virtual Node Parent { get; set; } = null;
    public int Left { get; set; }
    public int Right { get; set; }
    public ICollection<Node> Nodes { get; set; } = [];
}
