using AGK.Application.Dto;
using AGK.Application.Features.Common;
using AGK.Application.Reponse;
using MediatR;

namespace AGK.Application.Features.UserManagment.Queries;
internal sealed class GetPageUsersHandler : IRequestHandler<GetPage<UserDTO>, ApiResponse<Page<UserDTO>>>
{
	public async Task<ApiResponse<Page<UserDTO>>> Handle(GetPage<UserDTO> request, CancellationToken cancellationToken) {

		await Task.Run(() => { }, cancellationToken);
		var result = ApiResponse.Success<Page<UserDTO>>(
			new Page<UserDTO>(
				request.PageNumber,
				request.PageSize,
				2,
				new List<UserDTO>() {
					new(1,"Aaaa","",""),
					new(2,"Bbbb","","")
				}));

		return result;
	}
}
