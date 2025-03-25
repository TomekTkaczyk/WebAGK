using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebAGK.Shared.Abstractions;
using WebAGK.Shared.Abstractions.Auth;
using WebAGK.Shared.Abstractions.Messaging;
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Abstractions.Repositories;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Api;
using WebAGK.Shared.Infrastructure.Auth;
using WebAGK.Shared.Infrastructure.Contexts;
using WebAGK.Shared.Infrastructure.DAL;
using WebAGK.Shared.Infrastructure.DAL.Repositories;
using WebAGK.Shared.Infrastructure.Database;
using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.Messaging.Brokers;
using WebAGK.Shared.Infrastructure.Middleware;
using WebAGK.Shared.Infrastructure.Modules;
using WebAGK.Shared.Infrastructure.Services;
using WebAGK.Shared.Infrastructure.Time;

namespace WebAGK.Shared.Infrastructure;

public static class Extensions
{
	private const string CorsPolicy = "cors";
	private const string CorsFrontUrlHeaderPolicy = "cors-fronturl-header";
	private const string DocsVersion = "v0.01";
	private const string TitleApi = "WebAGK API";

	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration,
		IList<IModule> modules) {
		AddSingletons(services);
		AddScoped(services);
		AddTransients(services);

		services.AddDatabase<InfrastructureDbContext>(configuration);

		var _disableModules = new List<string>();
		foreach(var (_key, _value) in configuration.AsEnumerable()) {
			if(!_key.Contains(":module:enabled")) {
				continue;
			}

			if(_value != null && !bool.Parse(_value)) {
				_disableModules.Add(_key.Split(":")[0]);
			}
		}

		services.AddModuleInfo(modules);
		services.AddContexts();

		services.AddCors(cors =>
		{
			cors.AddPolicy(name:CorsPolicy, x =>
			{
				x.WithOrigins(configuration.GetSection("AllowedHost").Get<string>())
				 .AllowCredentials()
				 .WithMethods("POST", "PUT", "DELETE")
				 .WithHeaders("Content-Type", "Authorization");
			});
			cors.AddPolicy(name: CorsFrontUrlHeaderPolicy, x =>
			{
				x.WithOrigins(configuration.GetSection("AllowedHost").Get<string>())
				 .AllowCredentials()
				 .WithMethods("POST", "PUT", "DELETE")
				 .WithHeaders("Content-Type", "Authorization", "X-Frontend-Url", "X-ConfirmEmail-Url");
			});
		});
		services.AddAuth(configuration, modules);
		services.AddDatabase(configuration);
		services.AddErrorHandling();
		
		services.AddBackgroundServices(configuration);

		services.AddControllers(options => options.Filters.Add<VaidateModelAttribute>())
			.ConfigureApplicationPartManager(manager =>
			{
				var _removedParts = new List<ApplicationPart>();
				foreach(var _disableModule in _disableModules) {
					var _parts = manager.ApplicationParts.Where(x => x.Name.Contains(_disableModule, StringComparison.InvariantCultureIgnoreCase));
					_removedParts.AddRange(_parts);
				}

				foreach(var _part in _removedParts) {
					manager.ApplicationParts.Remove(_part);
				}

				manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
			});

		services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(swagger =>
		{
			swagger.CustomSchemaIds(x => x.FullName!.Replace("+","-"));
			swagger.SwaggerDoc(DocsVersion, new OpenApiInfo 
			{ 
				Title = TitleApi,
				Version = DocsVersion,
			});

			var _securityScheme = new OpenApiSecurityScheme
			{
				Name = "JWT Authentication",
				Description = "Enter your JWT token in this field",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT"
			};

			swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, _securityScheme);

			var _securityRequirement = new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = JwtBearerDefaults.AuthenticationScheme
						}
					},
					[]
				}
			};

			swagger.AddSecurityRequirement(_securityRequirement);
		});

		return services;
	}
	
	private static void AddSingletons(IServiceCollection services) {
		services.AddSingleton<IClock, UtcClock>();
		services.AddSingleton<ITokenValidator, TokenValidator>();
		services.AddSingleton<IEmailSenderFactory, EmailSenderFactory>();
	}

	private static void AddScoped(IServiceCollection services) {
		var _assemblies = AppDomain.CurrentDomain
			.GetAssemblies()
			.Where(x => x.GetName().Name.StartsWith("WebAGK", StringComparison.InvariantCultureIgnoreCase))
			.ToList();
		services.AddScoped<IStoredFileRepository, StoredFileRepository>();
		services.AddScoped<IMessageBus, MessageBus>();

		var _uow = _assemblies
			.SelectMany(a => a.GetTypes())
			.Where(t => t.IsClass && !t.IsAbstract)
			.Where(t => t.GetInterfaces()
				.Any(i => i.IsAssignableTo(typeof(IUnitOfWork))));
		
		Register(_uow, services);
	
		var _repositories = _assemblies
			.SelectMany(a => a.GetTypes())
			.Where(t => t.IsClass && !t.IsAbstract)
			.Where(t => t.GetInterfaces()
				.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)));
		Register(_repositories, services);
	}

	private static void Register(IEnumerable<Type> repositories, IServiceCollection services) {
		foreach (var _repoType in repositories) {
			var _interfaces = _repoType.GetInterfaces();
			foreach (var _interfaceType in _interfaces) {
				if (_interfaceType.IsGenericTypeDefinition) {
					var _closedInterface = _interfaceType.MakeGenericType(_repoType.GetGenericArguments());
					services.AddScoped(_closedInterface, _repoType);
				} else {
					services.AddScoped(_interfaceType, _repoType); 
				}
			}
		}
	}
	
	private static void AddTransients(IServiceCollection services) {
		services.AddTransient<SmtpEmailSender>();
		services.AddTransient<FakeEmailSender>();
	}

	public static IApplicationBuilder UseInfrastructure(
		this IApplicationBuilder app,
		IWebHostEnvironment environment) {
		app.UseErrorHandling();
		
		if(environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.RoutePrefix = "docs/swagger";
				x.SwaggerEndpoint($"/swagger/{DocsVersion}/swagger.json", TitleApi);
			});
			app.UseReDoc(x =>
			{
				x.RoutePrefix = "docs";
				x.SpecUrl($"/swagger/{DocsVersion}/swagger.json");
				x.DocumentTitle = TitleApi;
			});
		}
		
		app.UseMiddleware<RefreshTokenMiddleware>();
		app.UseAuthentication();

		app.UseRouting();
		app.UseCors(CorsPolicy);
		app.UseAuthorization();

		return app;
	}

	public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new() {
		var _options = new T();
		configuration.GetSection(sectionName).Bind(_options);

		return _options;
	}
}
