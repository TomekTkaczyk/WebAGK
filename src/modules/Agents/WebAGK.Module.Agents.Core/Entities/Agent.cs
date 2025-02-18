using WebAGK.Module.Agents.Core.ValueObjects;
using WebAGK.Shared.Abstractions.Entities;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Agents.Core.Entities;
public class Agent : EntityBase {
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Email Email { get; set; }
    public PersonalId PersonalId { get; set; }
    public TaxId TaxId { get; set; }
    public RpuId RpuId  {get; set;}
    public Address Address { get; set; }
    public bool IsCompany  { get; set; }
    public bool IsActive { get; set; }
}
