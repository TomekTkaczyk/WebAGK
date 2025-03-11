using WebAGK.Shared.Infrastructure.Entities;
using WebAGK.Shared.Infrastructure.Types;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Insurer : ActiveStatusEntity{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Node<Agent>> Structure { get; set; }

    private Insurer() {}
    
    public static Insurer Create(
        string name,
        string description = "") {
        return new Insurer() {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ActiveStatus = true
        };
    }
}