namespace WebAGK.Shared.Abstractions.Auth;
public interface ITokenValidator
{
	ITokenValidator GetToken(string token);
	ITokenValidator HasValidUser(Guid userId);
	ITokenValidator IsNotExpired();
	bool Validate();
}
