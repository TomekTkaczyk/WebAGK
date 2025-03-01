using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Structure(Insurer insurer) : EntityBase {
    public Insurer Insurer { get; private set; } = insurer;
    public ICollection<StructureNode> SubAgents { get; private set; } = new List<StructureNode>();
}

