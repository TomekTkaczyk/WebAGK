using Microsoft.EntityFrameworkCore;
using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class StructureNode : EntityBase {
    public Agent Agent { get; init; }
    public Agent UpLine { get; set; }
    
    public ICollection<StructureNode> SubAgents { get; } = new List<StructureNode>();

    protected StructureNode() { }

    public static StructureNode Create(Insurer insurer, Agent agent, Agent upLine) {
        return new StructureNode() {
            Agent = agent,
            UpLine = upLine,
        };
    }

}