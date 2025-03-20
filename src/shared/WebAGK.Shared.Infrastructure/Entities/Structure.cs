using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Shared.Infrastructure.Types;

public class Structure<T> : EntityBase where T : EntityBase {
    public ICollection<Node<T>> Nodes { get; init; } = [];

    public void AddNode(Node<T> node) {
        Nodes.Add(node);
    }

    public void ClearStructure() {
        Nodes.Clear();
    }

    public Node<T> GetNode(Guid id) {
        return FindNode(Nodes, id);
    }
    
    private Node<T> FindNode(ICollection<Node<T>> nodes,  Guid id) {
        var _result = default(Node<T>);
        foreach (var _node in nodes) {
            if (_node.Id == id) {
                _result = _node;
                break;
            }
            _result = FindNode(_node.Nodes, _node.Id);
            if (_result is not null) {
                break;
            }
        }
        
        return _result;
    }
}