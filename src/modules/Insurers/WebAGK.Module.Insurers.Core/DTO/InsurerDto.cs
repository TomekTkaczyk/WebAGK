using WebAGK.Module.Insurers.Core.Entities;

namespace WebAGK.Module.Insurers.Core.DTO;

public sealed class InsurerDto {
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool ActiveStatus {get; set;}
    
    public InsurerDto() { }

    public static InsurerDto Create(Insurer insurer) {
        return new InsurerDto() {
            Id = insurer.Id,
            Name = insurer.Name,
            ActiveStatus = insurer.ActiveStatus
        };
    }
}