using WebAGK.Module.Insurers.Core.DTO;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Node {
    public Guid InsurerId { get; set; }
    public Guid AgentId { get; set; }
    public Agent Agent  { get; set; }
    public Guid? ParentId { get; set; }
    public Agent Parent  { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    
    public ICollection<Node> Nodes { get; set; } = [];


}
