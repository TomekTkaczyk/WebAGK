using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Insurer : ActiveStatusEntity{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Node> Structure { get; set; }

    private Insurer() {}
    
    public static Insurer Create(
        string name,
        string description = "",
        ICollection<Node> structure = null) {
        return new Insurer() {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ActiveStatus = true,
            Structure = structure ?? new List<Node>()
        };
    }

    public ICollection<Node> GetStructure(ICollection<NodeDto> nodes) {
        var _result = new List<Node>();
        foreach (var _node in nodes) {
            _result.Add( new Node() {
                InsurerId = Id,
                Agent = _node is null ? null : Agent.Create(_node.Agent),
                Nodes = GetStructure(_node is null ? [] : _node.Nodes),
            });
        }
        
        return _result;
    }

    public void RenumberingStructure() {
        var _counter = 0;
        RenumberingStructure(Structure, ref _counter);
    }
    
    private void RenumberingStructure(ICollection<Node> nodes, ref int counter) {
        var _orderedNodes = nodes.OrderBy(x => x.Agent.Name);
        foreach (var _node in _orderedNodes) {
            _node.Left = ++counter;
            RenumberingStructure(_node.Nodes, ref counter);
            _node.Right = ++counter;
        }
    }
}