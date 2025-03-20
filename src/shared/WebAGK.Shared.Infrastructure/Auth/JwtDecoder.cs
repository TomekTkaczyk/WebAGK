using Microsoft.IdentityModel.JsonWebTokens;
using WebAGK.Shared.Infrastructure.Exceptions;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class JwtDecoder
{
	private static void DecodeJwtToken(string token)
	{
		var _handler = new JsonWebTokenHandler();

		if(!_handler.CanReadToken(token)) {
			throw new InvalidJwtException();
		}

		var _jsonToken = _handler.ReadJsonWebToken(token);

		Console.WriteLine("Claims from JWT token:");
		foreach(var _claim in _jsonToken.Claims) {
			Console.WriteLine($"Type: {_claim.Type}, Value: {_claim.Value}");
		}

		// Jeśli potrzebujesz konkretnego claimu:
		var _sub = _jsonToken.GetClaim("sub");
		Console.WriteLine($"Subject (sub): {_sub}");
	}
}
