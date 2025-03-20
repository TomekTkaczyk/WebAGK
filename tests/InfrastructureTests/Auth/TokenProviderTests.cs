using Microsoft.IdentityModel.JsonWebTokens;
using WebAGK.Shared.Infrastructure.Auth;

namespace InfrastructureTests.Auth;

public class TokenProviderTests(TestFixture testFixture) : IClassFixture<TestFixture>
{
	private readonly TokenProvider _provider = new(testFixture.AuthOptions, testFixture.Clock);
	private readonly JsonWebTokenHandler _handler = new();

	[Fact]
	public void Provider_GenerateAccessToken_return_valid_token()
	{
		var _id = Guid.NewGuid();
		var _claims = new Dictionary<string, IEnumerable<string>>
		{
			{ "role", ["someRole"] },
			{ "permissions", ["someClaim1", "someClaim2", "someClaim3"] },
			{ "otherclaim", ["someOtherClaim1"] }
		};

		var _accessToken = _provider.GenerateAccessToken(_id, _claims);

		Assert.NotNull(_accessToken);
		Assert.NotEmpty(_accessToken);
		if(!_handler.CanReadToken(_accessToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var _token = _handler.ReadJsonWebToken(_accessToken);
		Assert.Equal(_id.ToString(),_token.GetClaim("sub").Value);
		Assert.Equal("someRole",_token.GetClaim("role").Value);

		var _claimsArray = _token.Claims.Where(x => x.Type.Equals("permissions")).Select(x => x.Value).ToList();
		Assert.Equal(3, _claimsArray.Count);
		Assert.Contains("someClaim1", _claimsArray);
		Assert.Contains("someClaim2", _claimsArray);
		Assert.Contains("someClaim3", _claimsArray);

		_claimsArray = _token.Claims.Where(x => x.Type.Equals("otherclaim")).Select(x => x.Value).ToList();
		Assert.Single(_claimsArray);
		Assert.Contains("someOtherClaim1", _claimsArray);
	}

	[Fact]
	public void Provider_GenerateConfirmEmailToken_return_valid_token()
	{
		var _id = Guid.NewGuid();
		var _email = "somemail@email.io";
		var _confirmEmailToken = _provider.GenerateConfirmEmailToken(_id, _email);

		Assert.NotNull(_confirmEmailToken);
		Assert.NotEmpty(_confirmEmailToken);
		if(!_handler.CanReadToken(_confirmEmailToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var _token = _handler.ReadJsonWebToken(_confirmEmailToken);
		var _emailClaim = _token.GetClaim("email").Value;
		Assert.Equal(_id.ToString(), _token.GetClaim("sub").Value);
		Assert.NotNull(_emailClaim);
		Assert.Equal(_email, _emailClaim);
	}

	[Fact]
	public void Provider_GenerateRefreshToken_return_valid_token()
	{
		var _id = Guid.NewGuid();
		var _refreshToken = _provider.GenerateRefreshToken(_id);

		Assert.NotNull(_refreshToken);
		Assert.NotEmpty(_refreshToken);
		if(!_handler.CanReadToken(_refreshToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var _token = _handler.ReadJsonWebToken(_refreshToken);
		Assert.Equal(_id.ToString(), _token.GetClaim("sub").Value);
	}
}

