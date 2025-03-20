using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Abstractions.Services;
using System.Text;

namespace WebAGK.Shared.Infrastructure.Auth;
public static class Extensions
{
	public static IServiceCollection AddAuth(
		this IServiceCollection services,
		IConfiguration configuration,
		IList<IModule> modules,
		Action<JwtBearerOptions> optionsFactory = null)
	{
		var _authOptions = configuration.GetOptions<AuthOptions>(AuthOptions.Section);

		services.AddSingleton<ITokenProvider, TokenProvider>();
		services.AddSingleton<IEmailConfirmerFactory, EmailConfirmerFactory>();

		var _tokenValidationParameters = new TokenValidationParameters
		{
			RequireAudience = _authOptions.RequireAudience,
			ValidIssuer = _authOptions.ValidIssuer,
			ValidIssuers = _authOptions.ValidIssuers,
			ValidateActor = _authOptions.ValidateActor,
			ValidAudience = _authOptions.ValidAudience,
			ValidAudiences = _authOptions.ValidAudiences,
			ValidateAudience = _authOptions.ValidateAudience,
			ValidateIssuer = _authOptions.ValidateIssuer,
			ValidateLifetime = _authOptions.ValidateLifetime,
			ValidateTokenReplay = _authOptions.ValidateTokenReplay,
			ValidateIssuerSigningKey = _authOptions.ValidateIssuerSigningKey,
			SaveSigninToken = _authOptions.SaveSigninToken,
			RequireExpirationTime = _authOptions.RequireExpirationTime,
			RequireSignedTokens = _authOptions.RequireSignedTokens,
			ClockSkew = TimeSpan.Zero
		};

		if(string.IsNullOrWhiteSpace(_authOptions.IssuerSigningKey)) {
			throw new ArgumentException("Missing IssuerSigningKey in options.", nameof(_authOptions.IssuerSigningKey));
		}

		if(!string.IsNullOrWhiteSpace(_authOptions.AuthenticationType)) {
			_tokenValidationParameters.AuthenticationType = _authOptions.AuthenticationType;
		}

		_tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_authOptions.IssuerSigningKey));

		if(!string.IsNullOrWhiteSpace(_authOptions.NameClaimType)) {
			_tokenValidationParameters.NameClaimType = _authOptions.NameClaimType;
		}

		if(!string.IsNullOrWhiteSpace(_authOptions.RoleClaimType)) {
			_tokenValidationParameters.RoleClaimType = _authOptions.RoleClaimType;
		}

		services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o =>
			{
				o.Authority = _authOptions.Authority;
				o.Audience = _authOptions.Audience;
				o.MetadataAddress = _authOptions.MetadataAddress;
				o.SaveToken = _authOptions.SaveToken;
				o.RefreshOnIssuerKeyNotFound = _authOptions.RefreshOnIssuerKeyNotFound;
				o.RequireHttpsMetadata = _authOptions.RequireHttpsMetadata;
				o.IncludeErrorDetails = _authOptions.IncludeErrorDetails;
				o.TokenValidationParameters = _tokenValidationParameters;
				if(!string.IsNullOrWhiteSpace(_authOptions.Challenge)) {
					o.Challenge = _authOptions.Challenge;
				}

				// add cookies
				o.Events = new JwtBearerEvents
				{
					OnMessageReceived = (context) =>
					{
						var _cookieToken = context.Request.Cookies["accessToken"];
						if(!string.IsNullOrEmpty(_cookieToken)) {

							context.Token = _cookieToken;
						}
						return Task.CompletedTask;
					}
				};

				optionsFactory?.Invoke(o);
			});

		services.AddSingleton(_authOptions);
		services.AddSingleton(_tokenValidationParameters);

		services.AddSingleton<IAuthorizationHandler, PermissionOrRoleHandler>();

		services.AddAuthorization(auth =>
		{
			foreach(var _module in modules) {
				foreach(var _policy in _module.Policies) {
					var _policyName = $"{_module.Name}.{_policy}";

					auth.AddPolicy(_policyName, policy =>
					   policy.Requirements.Add(new PermissionOrRoleRequirement(
						[_policyName], []))
					);

					auth.AddPolicy(_policyName+"OrAdmin", policy =>
						policy.Requirements.Add(new PermissionOrRoleRequirement(
						[_policyName], ["Admin"]))
					);

					//auth.AddPolicy(policyName, policy =>
					//	policy.RequireClaim("permissions", policyName));
				}
			}
		});

		return services;
	}
}
