using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.Repositories;

namespace WebAGK.Module.Users.UseCases.Commands.ConfirmEmail;
internal class ConfirmEmailHandler(
	IUserRepository repository,
	IUserUnitOfWork unitOfWork,
	IEmailConfirmerFactory emailConfirmerFactory) : IRequestHandler<ConfirmEmailCommand>
{
	public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
	{
		var _confirmToken = DecodeJwt(request.Token);

		var _idClaim = _confirmToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
			?? throw new InvalidEmailTokenException();

		if(!Guid.TryParse(_idClaim, out var _id)) {
			throw new InvalidEmailTokenException();
		}

		var _emailClaim = _confirmToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value
			?? throw new InvalidEmailTokenException();

		var _user = await repository
            .Get(new ByIdSpecification<User>(_id))
            .SingleOrDefaultAsync(cancellationToken)
			?? throw new InvalidEmailTokenException();

		if(!_user.ActiveStatus) {
			throw new UserNotActiveException(_user.Id);
		}

		if(!_user.EmailToConfirm.Equals(_emailClaim)) {
			throw new InvalidEmailTokenException();
		}

		var _emailConfirmer = emailConfirmerFactory.GetEmailConfirmer(EmailConfirmTypes.Jwt);
		if(!_emailConfirmer.Confirm(_user.EmailConfirmToken, request.Token, _emailClaim)) {
			throw new UserEmailConfirmException();
		}

		_user.Email = _emailClaim;
		_user.EmailConfirm = true;

		repository.Update(_user);
		await unitOfWork.SaveChangesAsync(cancellationToken);
	}

	private static JwtSecurityToken DecodeJwt(string token)
	{
		var _tokenHandler = new JwtSecurityTokenHandler();

		if(!_tokenHandler.CanReadToken(token)) {
			throw new InvalidEmailTokenException();
		}

		return _tokenHandler.ReadJwtToken(token);
	}
}
