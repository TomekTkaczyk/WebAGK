namespace WebAGK.Shared.Infrastructure.Entities;

public class Node<T> : EntityBase where T : EntityBase {
    public T Value { get; init; }
    public Node<T> Parent { get; init; }
    public int Left { get; set; }
    public int Right { get; set; }
    public List<Node<T>> Nodes { get; } = [];
    
    private Node() {}

    public Node(T value, Node<T> parent = null) {
        Value = value;
        Parent = parent;
    }
}