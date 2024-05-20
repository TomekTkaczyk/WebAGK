using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using MediatR;

namespace AGK.Application.Features.UserManagment.Queries;

internal sealed class GetUserHandler : IRequestHandler<GetById<UserDTO>, ApiResponse<UserDTO>>
{
	public async Task<ApiResponse<UserDTO>> Handle(GetById<UserDTO> query, CancellationToken cancellationToken) {
		await Task.Run(() => { }, cancellationToken);
		var user = ApiResponse.Success(new UserDTO(1,"","",""));

		return user;
	}
}
