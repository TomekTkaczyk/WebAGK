namespace WebAGK.Shared.Abstractions.Entities;

public interface IActiveStatusEntity : IEntityBase {
    bool ActiveStatus { get; }
    void SetActiveStatus(bool activeStatus);
}