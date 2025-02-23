using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Module.Users.Core;
using WebAGK.Module.Users.Core.DAL;
using WebAGK.Module.Users.Core.Entities;
using WebAGK.Module.Users.Core.Repositories;
using WebAGK.Module.Users.UseCases.Specifications;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Infrastructure.Database;

namespace WebAGK.Module.Users.Api;

internal class UserModule : IModule
{
	public const string BasePath = "users-module";

	public string Name { get; } = "Users";

	public string Path => BasePath;

	public IEnumerable<string> Policies { get; } = ["UserManager"];

	public void Register(IServiceCollection services, IConfiguration configuration)
	{
		services.AddCore(configuration);

		services.AddMediatR(cfg =>
		{
			var _assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => {
				var _name = x.GetName().Name;
				return _name != null && _name.StartsWith("WebAGK.Module.Users.",
					StringComparison.CurrentCultureIgnoreCase);
			})
			.ToArray();
			cfg.RegisterServicesFromAssemblies(_assemblies);
		});

		//services.Scan(scan => scan
		//	.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
		//	.AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
		//	.AsImplementedInterfaces()
		//	.WithScopedLifetime()
		//);
		//services.Scan(scan => scan
		//	.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
		//	.AddClasses(classes => classes.AssignableTo(typeof(IRequest)))
		//	.AsImplementedInterfaces()
		//	.WithScopedLifetime()
		//);
	}

	public void Use(IApplicationBuilder app) {
		
		app.MigrateDatabase<UsersDbContext>();
		
		using var _scope = app.ApplicationServices.CreateScope();

		var _repository = _scope.ServiceProvider
			.GetRequiredService<IUserRepository>();
		var _passwordHasher = _scope.ServiceProvider
			.GetRequiredService<IPasswordHasher<User>>();
		var _configuration = _scope.ServiceProvider
			.GetRequiredService<IConfiguration>();
		var _unitOfWork = _scope.ServiceProvider
			.GetRequiredService<IUserUnitOfWork>();
		Task.Run(async () => await InitializeAdminAsync(_configuration, _repository, _unitOfWork, _passwordHasher))
			.Wait();
	}

	private static async Task InitializeAdminAsync(
		IConfiguration configuration, 
		IUserRepository repository, 
		IUserUnitOfWork unitOfWork,
		IPasswordHasher<User> passwordHasher) {

		var _user = await repository.Get(new UserByNameSpecification("Admin"))
			.SingleOrDefaultAsync();
		if (_user == null) {
			_user = new User
			{
				Id = Guid.NewGuid(),
				Name = "Admin",
				Password = passwordHasher.HashPassword(null!, ""),
				Role = "Admin",
				IsActive = true,
				Email = configuration.GetSection("AdminEmail").Value,
				EmailConfirm = true,
				Permissions = new Dictionary<string, IEnumerable<string>>()
				{
					{ "Users", new List<string> { "UserManager" } }
				}
			};

			repository.Add(_user);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
