using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Agent : ActiveStatusEntity, IEquatable<Agent> {
    public string Name { get; set; }

    private Agent(){}
    
    public static Agent Create(string name) {
        return new Agent() {
            Name = name,
        };
    }

    public bool Equals(Agent other) {
        return base.Equals(other);
    }
}