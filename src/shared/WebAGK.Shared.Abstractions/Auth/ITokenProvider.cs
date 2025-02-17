namespace WebAGK.Shared.Abstractions.Auth;

public interface ITokenProvider
{
	string GenerateAccessToken(Guid userId, IDictionary<string, IEnumerable<string>> claims);
	
	string GenerateRefreshToken(Guid userId);

	string GenerateConfirmEmailToken(Guid userId, string email);
}
