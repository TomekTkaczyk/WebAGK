namespace WebAGK.Shared.Abstractions.Auth;

public record EmailConfirmJwt
(
	string AccessToken,
	string Email
);
