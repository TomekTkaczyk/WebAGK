using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAGK.Shared.Infrastructure.CQRS;

public sealed record Page<T>(
	int PageNumber,
	int PageSize,
	int TotalCount,
	IReadOnlyCollection<T> Collection) {
	
	public static Page<T> Create(int pageNumber, int pageSize, int totalCount, IReadOnlyCollection<T> collection) 
		=> new Page<T>(pageNumber, pageSize, totalCount, collection);
};
