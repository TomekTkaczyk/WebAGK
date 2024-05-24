using AGK.Application.Auth;
using AGK.Domain.Entities;
using AGK.Domain.Repositories;

namespace AGK.Infrastructure.Auth;
public class UserManager : IUserManager {

	private readonly IUserRepository _userRepository;
	private readonly IPasswordManager _passwordManager;

	public UserManager(IUserRepository userRepository, IPasswordManager passwordManager) {
		_userRepository = userRepository;
		_passwordManager = passwordManager;
	}

	public IQueryable<User> Users => _userRepository.Get(null);

	public Task<User> CreateAsync(User user, CancellationToken cancellationToken) {
		user.PasswordHash = _passwordManager.HashPassword(user.PasswordHash);
	}
}
