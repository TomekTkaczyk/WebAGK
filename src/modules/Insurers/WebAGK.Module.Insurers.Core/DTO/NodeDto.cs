namespace WebAGK.Module.Insurers.Core.DTO;

public sealed record NodeDto(
    Guid InsurerId,
    AgentDto Parent,
    AgentDto Agent,
    int Left,
    int Right,
    ICollection<NodeDto> Nodes) {
};
    