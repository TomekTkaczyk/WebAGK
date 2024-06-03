using AGK.Application.Dto;

namespace AGK.Application.Services;

public interface IAuthenticator
{
    JwtDTO CreateToken(int userId, string role);
}
