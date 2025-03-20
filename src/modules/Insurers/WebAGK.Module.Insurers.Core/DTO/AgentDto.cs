namespace WebAGK.Module.Insurers.Core.DTO;

public sealed record AgentDto(
    Guid Id,
    string Name,
    bool ActiveStatus) {
};
