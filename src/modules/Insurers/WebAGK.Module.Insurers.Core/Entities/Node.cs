using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

internal class Node : EntityBase {
    
    private readonly List<Node> _nodes = [];
    public IReadOnlyCollection<Node> Nodes => _nodes.AsReadOnly();

    public Guid StructureId { get; private set; }
    public Structure Structure { get; set; }

    public Guid? ParentId { get; private set; }
    public Node Parent { get; private set; }
    
    public Guid AgentId { get; private set; }
    public Agent Agent { get; private set; }

    public int Left { get; set; }
    public int Right { get; set; }
    
    private Node() {}

    public Node(Structure structure, Agent agent, Node parent = null) {
        AgentId = agent.Id;
        Agent = agent;
        Parent = parent;
        ParentId = parent?.Id;
        StructureId = structure.Id;
        Structure = structure;
    }

    public void AddNode(Node node) {
        node.Parent = this;
        _nodes.Add(node);
    }
}