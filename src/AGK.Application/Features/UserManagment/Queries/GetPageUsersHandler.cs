using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Features.UserManagment.Specifications;
using AGK.Application.Mappers;
using AGK.Application.Reponse;
using AGK.Domain.Repositories;
using MediatR;

namespace AGK.Application.Features.UserManagment.Queries;
internal sealed class GetPageUsersHandler(IUserRepository repository)
	: IRequestHandler<GetPage<UserDTO>, ApiResponse<Page<UserDTO>>>
{
	private readonly IUserRepository _repository = repository;

	public async Task<ApiResponse<Page<UserDTO>>> Handle(GetPage<UserDTO> request, CancellationToken cancellationToken = default) {

		var specification = new UserSearchSpecification(request.ActiveStatus, request.SearchString);

		var query = _repository.Get(specification);
		var total = await _repository.CountAsync(query, cancellationToken);
		var list = await _repository.GetPageAsync(query, request.PageNumber, request.PageSize, cancellationToken);
		var result = list
			.Select(x => x.ToDTO())
			.ToList()
			.AsReadOnly();

		return ApiResponse.Success(new Page<UserDTO>(request.PageNumber, request.PageSize, total, result));
	}
}
