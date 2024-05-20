using AGK.Application.Auth;
using AGK.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AGK.Infrastructure.Auth;

public static class Extensions
{
	private const string _sectionName = "Auth";

	public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration) {

		services.Configure<AuthOptions>(configuration.GetRequiredSection(_sectionName));
		var options = configuration.GetOptions<AuthOptions>(_sectionName);

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
