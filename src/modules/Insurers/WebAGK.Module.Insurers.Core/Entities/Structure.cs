using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

internal class Structure : EntityBase {

    private readonly List<Node> _nodes = [];
    
    public IReadOnlyCollection<Node> Nodes => _nodes.AsReadOnly();

    public void AddNode(Node node) {
        _nodes.Add(node);
    }
    
    public void RenumberingStructure() {
        var _counter = 0;
        RenumberingStructure(_nodes, ref _counter);
    }

    public void Clear() {
        _nodes.Clear();
    }
    
    private static void RenumberingStructure(IReadOnlyCollection<Node> nodes, ref int counter) {
        var _orderedNodes = nodes.OrderBy(x => x.Agent.Name);
        foreach (var _node in _orderedNodes) {
            _node.Left = ++counter;
            RenumberingStructure(_node.Nodes, ref counter);
            _node.Right = ++counter;
        }
    }
}