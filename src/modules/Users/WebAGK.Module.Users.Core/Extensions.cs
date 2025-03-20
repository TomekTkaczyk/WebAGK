using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Users.Core.DAL;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Services;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Users.Core;

internal static class Extensions
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDatabase<UsersDbContext>(configuration);
		services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddScoped<IEmailVerificationService, EmailVerificationService>();

		return services;
	}
}
