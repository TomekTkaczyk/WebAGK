using AGK.Application.Specifications.Common;
using AGK.DataAccess;
using AGK.Domain.Entities;
using AGK.Infrastructure.Repositories;
using AGK.Tests.Fixtures;

namespace AGK.Tests.Application.Specifications;
public class CommonSpecificationTests
{
	private readonly AgkDbContext _context;
	private readonly UserRepository _userRepository;

	public CommonSpecificationTests()
    {
        _context = AgkDbContextFactory.CreateDbContext("server=localhost;user=root;password=123456;database=WebAGK");
        _userRepository = new UserRepository(_context);
    }

    [Fact]
    public void ByIdSpecification_should_return_the_admin_user() {

        var specification = new ByIdSpecification<User>(1);
        var users = _userRepository.Get(specification).ToList();

        Assert.NotNull(users);
		Assert.Single(users);
		Assert.Equal("Admin", users[0].UserName.Value);
	}

	[Fact]
	public void ByIdSpecification_should_return_the_Perlon_user() {

		var specification = new ByIdSpecification<User>(2);
		var users = _userRepository.Get(specification).ToList();

		Assert.NotNull(users);
		Assert.Single(users);
		Assert.Equal("Perlon", users[0].UserName.Value);
	}
}
