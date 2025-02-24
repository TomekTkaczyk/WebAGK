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
		var id = Guid.NewGuid();
		var claims = new Dictionary<string, IEnumerable<string>>
		{
			{ "role", ["someRole"] },
			{ "permissions", ["someClaim1", "someClaim2", "someClaim3"] },
			{ "otherclaim", ["someOtherClaim1"] }
		};

		var accessToken = _provider.GenerateAccessToken(id, claims);

		Assert.NotNull(accessToken);
		Assert.NotEmpty(accessToken);
		if(!_handler.CanReadToken(accessToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var token = _handler.ReadJsonWebToken(accessToken);
		Assert.Equal(id.ToString(),token.GetClaim("sub").Value);
		Assert.Equal("someRole",token.GetClaim("role").Value);

		var claimsArray = token.Claims.Where(x => x.Type.Equals("permissions")).Select(x => x.Value).ToList();
		Assert.Equal(3, claimsArray.Count);
		Assert.Contains("someClaim1", claimsArray);
		Assert.Contains("someClaim2", claimsArray);
		Assert.Contains("someClaim3", claimsArray);

		claimsArray = token.Claims.Where(x => x.Type.Equals("otherclaim")).Select(x => x.Value).ToList();
		Assert.Single(claimsArray);
		Assert.Contains("someOtherClaim1", claimsArray);
	}

	[Fact]
	public void Provider_GenerateConfirmEmailToken_return_valid_token()
	{
		var id = Guid.NewGuid();
		var email = "somemail@email.io";
		var confirmEmailToken = _provider.GenerateConfirmEmailToken(id, email);

		Assert.NotNull(confirmEmailToken);
		Assert.NotEmpty(confirmEmailToken);
		if(!_handler.CanReadToken(confirmEmailToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var token = _handler.ReadJsonWebToken(confirmEmailToken);
		var emailClaim = token.GetClaim("email").Value;
		Assert.Equal(id.ToString(), token.GetClaim("sub").Value);
		Assert.NotNull(emailClaim);
		Assert.Equal(email, emailClaim);
	}

	[Fact]
	public void Provider_GenerateRefreshToken_return_valid_token()
	{
		var id = Guid.NewGuid();
		var refreshToken = _provider.GenerateRefreshToken(id);

		Assert.NotNull(refreshToken);
		Assert.NotEmpty(refreshToken);
		if(!_handler.CanReadToken(refreshToken)) {
			throw new InvalidOperationException("Token cannot be read or is invalid.");
		}
		var token = _handler.ReadJsonWebToken(refreshToken);
		Assert.Equal(id.ToString(), token.GetClaim("sub").Value);
	}
}

