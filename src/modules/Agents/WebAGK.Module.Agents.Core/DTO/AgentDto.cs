using WebAGK.Module.Agents.Core.Entities;

namespace WebAGK.Module.Agents.Core.DTO;

public class AgentDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string TaxId { get; set; }
    public string PersonalId { get; set; }
    public bool IsCompany { get; set; } = false;
    public bool ActiveStatus { get; set; } = true;

    private AgentDto() { }
    
    public static AgentDto Create(Agent agent) {
        return new AgentDto() {
            Id = agent.Id,
            FirstName = agent.FirstName,
            SecondName = agent.SecondName,
            LastName = agent.LastName,
            TaxId = agent.TaxId,
            PersonalId = agent.PersonalId,
            IsCompany = agent.IsCompany,
            ActiveStatus = agent.ActiveStatus,
        };
    }
}