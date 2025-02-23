using WebAGK.Module.Agents.Core.ValueObjects;
using WebAGK.Shared.Infrastructure.Entities;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Module.Agents.Core.Entities;
public class Agent : ActiveStatusEntity {
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string PhoneNumber { get; set; }
    public Email Email { get; set; }
    public PersonalId PersonalId { get; set; }
    public TaxId TaxId { get; set; }
    public RpuId RpuId  {get; set;}
    public Address Address { get; set; }
    public bool IsCompany  { get; set; }
    public bool IsActive { get; set; }
    public string Description {get; set;}

    private Agent() {}

    public static Agent Create(
        string lastName,
        string firstName,
        string secondName,
        string personalId,
        string taxId,
        bool isCompany
    ) {
        return new Agent() {
            Id = Guid.NewGuid(),
            LastName = lastName,
            FirstName = firstName,
            SecondName = secondName,
            PersonalId = personalId,
            TaxId = taxId,
            IsCompany = isCompany,
            IsActive = true
        };
    }
}
