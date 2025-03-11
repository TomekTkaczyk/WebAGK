using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.DbModels;

public class NodeDb {
    public Guid Id { get; set; }
	
    public Guid InsurerId { get; set; }
    public InsurerDb Insurer { get; set; }
    
    public Guid? ParentId { get; set; }
    public virtual NodeDb Parent { get; set; }
	
    public Guid? AgentId { get; set; }
    public virtual Agent Agent { get; set; }
    
    public int Left	{ get; set; }
    
    public int Right	{ get; set; }
	
    public ICollection<NodeDb> Nodes { get; set; }
}