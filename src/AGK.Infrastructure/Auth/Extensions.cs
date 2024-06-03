using AGK.Application.Services;
using AGK.Domain.Entities;
using AGK.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AGK.Infrastructure.Auth;

internal static class Extensions
{

	internal static IServiceCollection AddAuth(this IServiceCollection services) {

		services
			.AddSingleton<IAuthenticator, Authenticator>()
			.AddSingleton<IPasswordManager, PasswordManager>()
			.AddSingleton<IPasswordHasher<IBaseEntity>, PasswordHasher<IBaseEntity>>()
			.AddScoped<IUserManager, UserManager>()
			.AddScoped<CurrentUserService>();
			

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
