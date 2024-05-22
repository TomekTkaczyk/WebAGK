using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Features.UserManagment.Specifications;
using AGK.Application.Mappers;
using AGK.Application.Reponse;
using AGK.Domain.Repositories;
using MediatR;

namespace AGK.Application.Features.UserManagment.Queries;
internal sealed class GetPageUsersHandler : IRequestHandler<GetPage<UserDTO>, ApiResponse<Page<UserDTO>>>
{
	private readonly IUserRepository _repository;

	public GetPageUsersHandler(IUserRepository repository)
    {
		_repository = repository;
	}

    public async Task<ApiResponse<Page<UserDTO>>> Handle(GetPage<UserDTO> request, CancellationToken cancellationToken = default) {

		await Task.Run(() => { }, cancellationToken);

		var specification = new UserSearchSpecification(
			request.ActiveStatus,
			request.SearchString);

		var query = _repository.Get(specification);
		var total = await _repository.CountAsync(query, cancellationToken);
		var page = (await _repository.GetPageAsync(query, request.PageNumber, request.PageSize, cancellationToken))
			.Select(x => x.ToDTO())
			.ToList()
			.AsReadOnly();

		return ApiResponse.Success(new Page<UserDTO>(request.PageNumber, request.PageSize, total, page));
	}
}
