using AGK.Application.Dto;
using AGK.Application.Features.Common;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;
internal sealed class DeleteHandler : IRequestHandler<Delete<UserDTO>>
{
	public Task Handle(Delete<UserDTO> request, CancellationToken cancellationToken) {
		throw new NotImplementedException();
	}
}
