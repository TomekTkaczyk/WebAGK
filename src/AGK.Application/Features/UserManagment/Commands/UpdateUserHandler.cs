using AGK.Application.Reponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Application.Features.UserManagment.Commands;
public sealed class UpdateUserHandler() : IRequestHandler<UpdateUser, ApiResponse>
{
	public Task<ApiResponse> Handle(UpdateUser request, CancellationToken cancellationToken) {
		throw new NotImplementedException();
	}
}
