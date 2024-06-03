using AGK.Application.Dto;
using AGK.Application.Reponse;
using AGK.Domain.Entities;
using AGK.Domain.ValueObjects;
using MediatR;

namespace AGK.Application.Features.Common
{
	public record GetPage<T> : IRequest<ApiResponse<Page<T>>> where T : DTO
	{
		private bool? _activeStatus;
		private string _searchString;
		private PageNumber _pageNumber;
		private PageSize _pageSize;

        public GetPage(string searchString, bool? activeStatus, int? pageNumber, int? pageSize)
        {			
			_searchString = searchString?.ToUpper();
			_activeStatus = activeStatus;
			_pageNumber = pageNumber ?? 1;
			_pageSize = pageSize ?? 0;
        }

        public string SearchString { get => _searchString; init =>  _searchString = value; }
		public bool? ActiveStatus { get => _activeStatus; init => _activeStatus = value; }
		public PageNumber PageNumber { get => _pageNumber; init => _pageNumber = value; }
		public PageSize PageSize { get => _pageSize; init => _pageSize = value; }
	}
}
