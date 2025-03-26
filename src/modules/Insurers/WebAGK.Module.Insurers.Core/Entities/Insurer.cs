using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

internal class Insurer : ActiveStatusEntity {

    public string Name { get; set; }
    public string Description { get; set; }
    public Structure Structure { get; private set; } = new();
    

    private Insurer(string name) {
        Name = name;
    }

    public static Insurer Create(
        string name,
        string description = "") {

        var _insurer = new Insurer(name) {
            Description = description,
            ActiveStatus = true,
        };
       
        return _insurer;
    }
}