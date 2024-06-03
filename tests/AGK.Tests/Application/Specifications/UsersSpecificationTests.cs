using AGK.Application.Features.UserManagment.Specifications;
using AGK.DataAccess;
using AGK.Infrastructure.Repositories;
using AGK.Tests.Fixtures;

namespace AGK.Tests.Application.Specifications;
public class UsersSpecificationTests
{
	private readonly AgkDbContext _context;
	private readonly UserRepository _userRepository;

	public UsersSpecificationTests() {
		_context = AgkDbContextFactory.CreateDbContext("server=localhost;user=root;password=123456;database=WebAGK");
		_userRepository = new UserRepository(_context);
	}

	[Fact]
	public void UserSearchSpecification_should_by_return_all_users() {

		var specification = new UserSearchSpecification();
		var users = _userRepository.Get(specification).ToList();

		Assert.NotNull(users);
		Assert.Contains("Admin",users.Select(x => x.UserName.Value));
		Assert.Contains("Perlon",users.Select(x => x.UserName.Value));
		Assert.Contains("KS",users.Select(x => x.UserName.Value));
		Assert.Contains("GS",users.Select(x => x.UserName.Value));
	}

	[Fact]
	public void UserSearchSpecification_should_by_return_admin_user() {

		var specification = new UserSearchSpecification(null, "Admin");
		var users = _userRepository.Get(specification).ToList();

		Assert.NotNull(users);
		Assert.Single(users);
		Assert.Equal("Admin", users[0].UserName.Value);
	}

	[Fact]
	public void UserSearchSpecification_should_by_return_KS_user() {

		var specification = new UserSearchSpecification(null, "K");
		var users = _userRepository.Get(specification).ToList();

		Assert.NotNull(users);
		Assert.Single(users);
		Assert.Equal("KS", users[0].UserName.Value);
	}

	[Fact]
	public void UserSearchSpecification_should_by_return_KS_and_GS_users() {

		var specification = new UserSearchSpecification(null, "S");
		var users = _userRepository.Get(specification).ToList();

		Assert.NotNull(users);
		Assert.Equal(2,users.Count);
		Assert.Equal("KS", users[0].UserName.Value);
		Assert.Equal("GS", users[1].UserName.Value);
	}
}
