namespace WebAGK.Module.Insurers.Core.DTO;

public sealed record NodeDto(
    Guid Id,
    Guid? ParentId,
    AgentDto Agent,
    int Left,
    int Right,
    ICollection<NodeDto> Nodes) {

};
    