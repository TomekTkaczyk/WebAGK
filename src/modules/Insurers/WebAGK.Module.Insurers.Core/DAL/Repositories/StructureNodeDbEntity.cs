using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.Repositories;

public class StructureNodeDbEntity : StructureNode {
    public int Left {get; set;}
    public int Right {get; set;}
}