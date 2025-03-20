using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

internal class Insurer : ActiveStatusEntity{
    
    public string Name { get; set; }
    public string Description { get; set; }
    public Structure Structure { get; set; } = new();

    private Insurer() {}
    
    public static Insurer Create(
        string name,
        string description = "") {
        var _id = Guid.NewGuid();
        var _insurer = new Insurer() {
            Id = _id,
            Name = name,
            Description = description,
            ActiveStatus = true,
        };

        return _insurer;
    }

    // public ICollection<Node> GetStructure(ICollection<NodeDto> nodes) {
    //     var _result = new List<Node>();
    //     foreach (var _node in nodes) {
    //         _result.Add( new Node() {
    //             InsurerId = Id,
    //             Agent = _node is null ? null : Agent.Create(_node.Agent),
    //             Nodes = GetStructure(_node is null ? [] : _node.Nodes),
    //         });
    //     }
    //     
    //     return _result;
    // }

    
    public void RenumberingStructure() {
        var _counter = 0;
        RenumberingStructure(Structure.Nodes, ref _counter);
    }

    private static void RenumberingStructure(ICollection<Node<Agent>> nodes, ref int counter) {
        var _orderedNodes = nodes.OrderBy(x => x.Value.Name);
        foreach (var _node in _orderedNodes) {
            _node.Left = ++counter;
            RenumberingStructure(_node.Nodes, ref counter);
            _node.Right = ++counter;
        }
    }
}