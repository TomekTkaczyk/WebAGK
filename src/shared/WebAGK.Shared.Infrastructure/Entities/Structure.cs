using WebAGK.Shared.Infrastructure.ValueObjects;

namespace WebAGK.Shared.Infrastructure.Entities;

public class Structure<T> : EntityBase where T : EntityBase {
    public ICollection<Node<T>> Nodes { get; init; } = new List<Node<T>>();
}