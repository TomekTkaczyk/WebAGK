namespace AGK.Domain.Entities;

public interface IActiveStatus : IBaseEntity
{
	bool ActiveStatus { get; }

	void SetActiveStatus(bool activeStatus);
}
