using AGK.Application.Dto;
using AGK.Application.Features.Common;
using MediatR;

namespace AGK.Application.Features.UserManagment.Commands;
internal sealed class SetActiveHandler : IRequestHandler<SetActive<UserDTO>>
{
	public Task Handle(SetActive<UserDTO> request, CancellationToken cancellationToken) {
		throw new NotImplementedException();
	}
}
