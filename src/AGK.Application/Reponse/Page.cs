namespace AGK.Application.Reponse;

public record Page<T>(
	int PageNumber,
	int PageSize,
	int TotalCount,
	IReadOnlyCollection<T> Collection);
