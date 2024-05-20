namespace AGK.Domain.Entities;

public interface IActiveStatus
{
	bool ActiveStatus { get; }

	void SetActiveStatus(bool activeStatus);
}
