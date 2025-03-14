namespace WebAGK.Shared.Infrastructure.Types;

public abstract class Node<T> where T : class {
    public T Value { get; set; }
    public Node<T> Parent { get; set; } = null;
    public ICollection<Node<T>> Nodes { get; set; } = [];
}