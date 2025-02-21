using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Services;
using WebAGK.Module.Users.Core.DTO;
using WebAGK.Module.Users.Core.Exceptions;
using WebAGK.Module.Users.Core.Repositories;

namespace WebAGK.Module.Users.Core.Services;
internal class EmailVerificationService(
	IUserRepository userRepository,
	IEmailConfirmerFactory emailConfirmerFactory) : IEmailVerificationService
{
	public async Task Confirm(ConfirmEmailDto dto, CancellationToken cancellationToken)
	{
		//ConfirmToken decode !!! and check compliance !!! 

		var _user = await userRepository.GetByEmailAsync(dto.Email, cancellationToken)
			?? throw new InvalidCredentialsException();

		if(!_user.IsActive) {
			throw new UserNotActiveException(_user.Id);
		}

		var _emailConfirmer = emailConfirmerFactory.GetEmailConfirmer();
		if(!_emailConfirmer.Confirm(_user.EmailConfirmToken, dto.ConfirmToken, dto.Email)) {
			throw new UserEmailConfirmException();
		}

		_user.Email = dto.Email;
		_user.EmailConfirm = true;
		_user.EmailConfirmToken = null;

		await userRepository.UpdateAsync(_user, cancellationToken);
	}
}
