using WebAGK.Module.Insurers.Core.DTO;
using WebAGK.Shared.Infrastructure.Entities;

namespace WebAGK.Module.Insurers.Core.Entities;

public class Agent : ActiveStatusEntity {
    public string Name { get; private set; }

    public static Agent Create(AgentDto agent) {
        return new Agent() {
            Name = agent.Name,
            Id = agent.Id,
            ActiveStatus = agent.ActiveStatus
        };
    }
}