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
	IUserRepository userRepository,
	CurrentUserService currentUserService) : IUserManager {

	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IUserRepository _userRepository = userRepository;
	private readonly CurrentUserService _currentUserService = currentUserService;

	public IQueryable<User> Users => _userRepository.Get();

	public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default) {

		User currentUser = null;
		try {
			var name = _currentUserService.GetCurrentUser().UserName;
			currentUser = await FindByUserNameAsync(new Name(name), cancellationToken);
		}
		catch {
		}
		var createdUser = _userRepository.Add(user);
		
		await _unitOfWork.SaveChangesAsync(currentUser, cancellationToken);

		return createdUser;
	}

	public async Task<int> UpdateAsync(User user, CancellationToken cancellationToken = default) {

		User currentUser = null;

		try {
			var name = _currentUserService.GetCurrentUser().UserName;
			currentUser = await FindByUserNameAsync(new Name(name), cancellationToken);
		}
		catch {
		}

		var updatedUser = await _userRepository
			.Get(new ByIdSpecification<User>(user.Id))
			.SingleOrDefaultAsync(cancellationToken)
		?? throw new UserNotFoundException();

		updatedUser.Update(user.FirstName, user.LastName, user.Email);

		_userRepository.Update(user);
		
		return await _unitOfWork.SaveChangesAsync(currentUser, cancellationToken);
	}

	public async Task<int> DeleteAsync(User user, CancellationToken cancellationToken = default) {

		var deletedUser = await _userRepository
			.Get(new ByIdSpecification<User>(user.Id))
			.SingleOrDefaultAsync(cancellationToken)
		?? throw new UserNotFoundException();

		_userRepository.Delete(deletedUser);

		return await _unitOfWork.SaveChangesAsync(null, cancellationToken);
	}

	public async Task<User> FindByIdAsync(EntityId id, CancellationToken cancellationToken = default) 
		=> await Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<User> FindByEmailAsync(Email email, CancellationToken cancellationToken = default) 
		=> await Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<User> FindByUserNameAsync(Name userName, CancellationToken cancellationToken = default) 
		=> await Users.SingleOrDefaultAsync(x => x.UserName == userName, cancellationToken)
		?? throw new UserNotFoundException();

	public async Task<bool> IsUnique(User user, CancellationToken cancellationToken = default) {
		var taskName = _userRepository.Get().FirstOrDefaultAsync(x => x.UserName == user.UserName, cancellationToken);
		var taskEmail = _userRepository.Get().FirstOrDefaultAsync(x => x.Email == user.Email, cancellationToken);
		var result = await Task.WhenAll(taskName, taskEmail);

		return result.IsNullOrEmpty();
	}
}
