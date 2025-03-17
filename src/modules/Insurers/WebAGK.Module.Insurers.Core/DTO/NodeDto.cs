namespace WebAGK.Module.Insurers.Core.DTO;

public sealed record NodeDto(
    AgentDto Agent,
    int Left,
    int Right,
    ICollection<NodeDto> Nodes) {
};
    