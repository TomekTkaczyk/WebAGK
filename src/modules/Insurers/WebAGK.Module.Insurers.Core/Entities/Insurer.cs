using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Insurer : ActiveStatusEntity{
    public string Name { get; set; }

    private Insurer() {}
    
    public static Insurer Create(
        string name) {
        return new Insurer() {
            Id = Guid.NewGuid(),
            Name = name,
            ActiveStatus = true
        };
    }
}