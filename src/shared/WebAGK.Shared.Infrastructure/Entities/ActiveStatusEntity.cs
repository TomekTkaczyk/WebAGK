using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Infrastructure.Entities;

public abstract class ActiveStatusEntity : EntityBase, IActiveStatusEntity {
    public bool ActiveStatus { get; private set; }
    public void SetActiveStatus(bool activeStatus) {
        ActiveStatus = activeStatus;
    }
}