namespace WebAGK.Shared.Infrastructure.Types;

public class Node<T> where T : class {
    public T Value { get; set; }
    public Node<T> Parent { get; set; } = null;
    public List<Node<T>> Nodes { get; set; } = [];
}