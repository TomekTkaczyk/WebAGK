using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.DAL.DbModels;

public class InsurerDb : EntityBase {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<NodeDb> Structure { get; set; }
}