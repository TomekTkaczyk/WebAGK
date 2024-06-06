using AGK.Application.Services;
using AGK.Application.Specifications.Common;
using AGK.Domain.Entities;
using AGK.Domain.Exceptions.Users;
using AGK.Domain.Repositories;
using AGK.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AGK.Infrastructure.Auth;

public class UserManager(
	IUnitOfWork unitOfWork, 
	IUserRepository userRepository) : IUserManager {

	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IUserRepository _userRepository = userRepository;

	public IQueryable<User> Users => _userRepository.Get();

	public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default) {

		var createdUser = _userRepository.Add(user);
		
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return createdUser;
	}

	public async Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default) {

		var updatedUser = await _userRepository
			.Get(new ByIdSpecification<User>(user.Id))
			.SingleAsync(cancellationToken)
		?? throw new UserNotFoundException();

		updatedUser.Update(user.FirstName, user.LastName, user.Email);

		_userRepository.Update(updatedUser);
		
		return await _unitOfWork.SaveChangesAsync(cancellationToken);
	}

	public async Task<int> DeleteAsync(User user, CancellationToken cancellationToken = default) {

		var deletedUser = await _userRepository
			.Get(new ByIdSpecification<User>(user.Id))
			.SingleOrDefaultAsync(cancellationToken)
		?? throw new UserNotFoundException();

		_userRepository.Delete(deletedUser);

		return await _unitOfWork.SaveChangesAsync(cancellationToken);
	}

	public async Task<User> FindByIdAsync(EntityId id, CancellationToken cancellationToken = default) 
		=> await Users.SingleAsync(x => x.Id == id, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<User> FindByEmailAsync(Email email, CancellationToken cancellationToken = default)
		=> await Users.SingleAsync(x => x.Email == email, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<User> FindByUserNameAsync(UserName userName, CancellationToken cancellationToken = default)
		=> await Users.SingleAsync(x => x.UserName == userName, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<bool> IsUniqueAsync(User user, CancellationToken cancellationToken = default) {
		var name = await _userRepository.Get().FirstOrDefaultAsync(x => x.UserName == user.UserName, cancellationToken);
		if (name is not null) {
			return false;
		}
		var email = await _userRepository.Get().FirstOrDefaultAsync(x => x.Email == user.Email, cancellationToken);
		if(email is not null) {
			return false;
		}

		return true;
	}
}
