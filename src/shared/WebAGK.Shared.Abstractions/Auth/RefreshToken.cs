namespace WebAGK.Shared.Abstractions.Auth;

public record RefreshToken(string Token, DateTime CreatedAt, DateTime Expires);
