namespace WebAGK.Shared.Abstractions.Entities;

public interface IEntityBase{
    public Guid Id { get; }
    public Guid ConcurrencyStamp { get; }
}