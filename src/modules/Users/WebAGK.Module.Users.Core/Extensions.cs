using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Shared.Infrastructure.Database;
using WebAGK.Module.Users.Core.DAL;
using WebAGK.Module.Users.Core.DAL.Repositories;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.Core.Services;

namespace WebAGK.Module.Users.Core;

internal static class Extensions
{
	public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
		services.AddScoped<IDataInitializerService, DataInitializerService>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IEmailVerificationService, EmailVerificationService>();
		services.AddDatatabase<UsersDbContext>(configuration);

		return services;
	}
}
