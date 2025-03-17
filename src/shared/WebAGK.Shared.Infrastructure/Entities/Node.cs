namespace WebAGK.Shared.Infrastructure.Entities;

public class Node<T> where T : EntityBase {
    public T Value { get; init; }
    public Node<T> Parent { get; init; }
    public ICollection<Node<T>> Nodes { get; init; }
}