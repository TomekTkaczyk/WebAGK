using AGK.Application.Reponse;
using MediatR;

namespace AGK.Application.Features.Common;

public sealed record GetPage<T> : IRequest<ApiResponse<Page<T>>>
{
	private string _searchString;

	public bool? ActiveStatus { get; init; }
	public string SearchString { get => _searchString; init => _searchString = value?.ToUpper(); }
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 0;

}
