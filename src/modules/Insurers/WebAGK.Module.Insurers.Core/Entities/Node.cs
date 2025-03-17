using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Node : EntityBase{
    public Guid InsurerId { get; set; }
    public virtual Insurer Insurer  { get; set; }
    public Guid AgentId { get; set; }
    public virtual Agent Agent  { get; set; }
    public Guid? ParentId { get; set; }
    public virtual Node Parent  { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    
    public ICollection<Node> Nodes { get; set; } = [];


}
