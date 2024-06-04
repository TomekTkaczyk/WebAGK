namespace AGK.Infrastructure.Auth;
public sealed record UserSession(TypeEntityId Id = default, string UserName = default, bool IsAuthenticated = default);
