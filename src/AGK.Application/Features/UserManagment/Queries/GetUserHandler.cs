using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using AGK.Application.Specifications;
using AGK.Application.Specifications.Common;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using MediatR;

namespace AGK.Application.Features.UserManagment.Queries;

internal sealed class GetUserHandler(IUserRepository userRepository) 
	: IRequestHandler<GetById<UserDTO>, ApiResponse<UserDTO>>
{
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<ApiResponse<UserDTO>> Handle(GetById<UserDTO> query, CancellationToken cancellationToken) {

		var specification = new ByIdSpecification<User>(query.Id);
		var user = _userRepository.GetPageAsync(specification, 1, 0, cancellationToken);
		await Task.Run(() => { }, cancellationToken);
		var response = ApiResponse.Success(new UserDTO(1,"","",""));

		return response;
	}
}
