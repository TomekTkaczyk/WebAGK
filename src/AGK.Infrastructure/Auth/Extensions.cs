using AGK.Application.Auth;
using AGK.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AGK.Infrastructure.Auth;

internal static class Extensions
{
	private const string _authSectionName = "Auth";

	internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration) {

		var authOptions = configuration.GetOptions<AuthOptions>(_authSectionName);

		services
			.AddSingleton<IAuthenticator, Authenticator>()
			.AddSingleton<IPasswordManager, PasswordManager>()
			.AddSingleton<IPasswordHasher<IBaseEntity>, PasswordHasher<IBaseEntity>>();
			//.AddAuthorization()
			//.AddAuthentication(x => {
			//	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			//	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			//})
			//.AddJwtBearer(x => {
			//	x.Audience = options.Audience;
			//	x.IncludeErrorDetails = true;
			//	x.TokenValidationParameters = new TokenValidationParameters {
			//		ValidIssuer = options.Issuer,
			//		ClockSkew = TimeSpan.FromSeconds(10),
			//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
			//	};
			//});

		return services;
	}
}
