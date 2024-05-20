using AGK.Application.Dto;

namespace AGK.Application.Auth;

public interface IAuthenticator
{
	JwtDto CreateToken(int userId, string role);
}
