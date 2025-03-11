namespace WebAGK.Shared.Infrastructure.Types;

public class Structure<T> where T : class {
    
    public List<Node<T>> Nodes { get; set; } = [];
}