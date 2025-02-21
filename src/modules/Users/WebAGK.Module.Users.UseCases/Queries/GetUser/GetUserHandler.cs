using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.DTO;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Queries.GetUser;
internal sealed class GetUserHandler(IUserRepository repository) : IRequestHandler<GetUserQuery, UserDto>
{
	public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken) {
		
		// var user = await repository.GetAsync(request.Id, cancellationToken);

		var specification = new ByIdSpecification<User>(request.Id);
		var user = await repository
			.Get(specification)
			.SingleOrDefaultAsync(cancellationToken);
		
		return UserDto.Create(user);
	}
}
