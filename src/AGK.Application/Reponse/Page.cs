using System.Collections.Generic;

namespace AGK.Application.Reponse;

public record Page<T>(
	int PageNumber,
	int PageSize,
	int TotalCount,
	IEnumerable<T> Collection);
