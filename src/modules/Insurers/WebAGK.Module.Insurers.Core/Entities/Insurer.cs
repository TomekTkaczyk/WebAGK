using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Insurer : ActiveStatusEntity{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Node> Structure { get; set; }

    private Insurer() {}
    
    public static Insurer Create(
        string name,
        string description = "") {
        return new Insurer() {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ActiveStatus = true,
            Structure = []
        };
    }

    public void RenumberingStructure() {
        var _counter = 0;
        foreach (var _node in Structure) {
            RenumberingNode(_node, ref _counter);
        }
    }

    private void RenumberingNode(Node node, ref int counter) {
        node.Left = ++counter;
        foreach (var _node in node.Nodes) {
            _node.Left = ++counter;
            RenumberingNode(_node, ref counter);
            _node.Right = ++counter;
        }
        node.Right = ++counter;
    }
}