using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Mappers;
using AGK.Application.Reponse;
using AGK.Application.Specifications.Common;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AGK.Application.Features.UserManagment.Queries;

internal sealed class GetUserHandler(IUserRepository userRepository) 
	: IRequestHandler<GetById<UserDTO>, ApiResponse<UserDTO>>
{
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<ApiResponse<UserDTO>> Handle(GetById<UserDTO> query, CancellationToken cancellationToken) {

		var specification = new ByIdSpecification<User>(query.Id);
		var user = await _userRepository.GetPage(specification, 1, 0).SingleAsync(cancellationToken);
		
		return ApiResponse.Success(user.ToDTO());
	}
}
