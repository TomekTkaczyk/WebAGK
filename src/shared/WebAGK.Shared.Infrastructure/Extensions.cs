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
using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Abstractions.Services;
using WebAGK.Shared.Infrastructure.Api;
using WebAGK.Shared.Infrastructure.Auth;
using WebAGK.Shared.Infrastructure.Contexts;
using WebAGK.Shared.Infrastructure.DAL;
using WebAGK.Shared.Infrastructure.DAL.Repositories;
using WebAGK.Shared.Infrastructure.Database;
using WebAGK.Shared.Infrastructure.Exceptions;
using WebAGK.Shared.Infrastructure.Middleware;
using WebAGK.Shared.Infrastructure.Modules;
using WebAGK.Shared.Infrastructure.Services;
using WebAGK.Shared.Infrastructure.Time;

namespace WebAGK.Shared.Infrastructure;

public static class Extensions
{
	private const string _corsPolicy = "cors";
	private const string _corsFrontUrlHeaderPolicy = "cors-fronturl-header";
	private const string _docsVersion = "v0.01";
	private const string _titleApi = "WebAGK API";

	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration,
		IList<IModule> modules)
	{
		AddScoped(services);
		AddSingletons(services);
		AddTransients(services);

		services.AddDatatabase<InfrastructureDbContext>(configuration);

		var disableModules = new List<string>();
		foreach(var (key, value) in configuration.AsEnumerable()) {
			if(!key.Contains(":module:enabled")) {
				continue;
			}

			if(value != null && !bool.Parse(value)) {
				disableModules.Add(key.Split(":")[0]);
			}
		}

		services.AddModuleInfo(modules);
		services.AddContexts();

		services.AddCors(cors =>
		{
			cors.AddPolicy(name:_corsPolicy, x =>
			{
				x.WithOrigins(configuration.GetSection("AllowedHost").Get<string>())
				 .AllowCredentials()
				 .WithMethods("POST", "PUT", "DELETE")
				 .WithHeaders("Content-Type", "Authorization");
			});
			cors.AddPolicy(name: _corsFrontUrlHeaderPolicy, x =>
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
				var removedParts = new List<ApplicationPart>();
				foreach(var disableModule in disableModules) {
					var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disableModule, StringComparison.InvariantCultureIgnoreCase));
					removedParts.AddRange(parts);
				}

				foreach(var part in removedParts) {
					manager.ApplicationParts.Remove(part);
				}

				manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
			});

		services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(swagger =>
		{
			swagger.CustomSchemaIds(x => x.FullName!.Replace("+","-"));
			swagger.SwaggerDoc(_docsVersion, new OpenApiInfo 
			{ 
				Title = _titleApi,
				Version = _docsVersion,
			});

			var securityScheme = new OpenApiSecurityScheme
			{
				Name = "JWT Authentication",
				Description = "Enter your JWT token in this field",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT"
			};

			swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

			var securityRequirement = new OpenApiSecurityRequirement
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

			swagger.AddSecurityRequirement(securityRequirement);
		});

		return services;
	}


	private static void AddScoped(IServiceCollection services)
	{
		services.AddScoped<IStoredFileRepository, StoredFileRepository>();
	}

	private static void AddSingletons(IServiceCollection services)
	{
		services.AddSingleton<IClock, UtcClock>();
		services.AddSingleton<ITokenValidator, TokenValidator>();
		services.AddSingleton<IEmailSenderFactory, EmailSenderFactory>();
	}

	private static void AddTransients(IServiceCollection services)
	{
		services.AddTransient<SmtpEmailSender>();
		services.AddTransient<FakeEmailSender>();
	}

	public static IApplicationBuilder UseInfrastructure(
		this IApplicationBuilder app,
		IWebHostEnvironment environment)
	{
		app.UseErrorHandling();
		
		if(environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.RoutePrefix = "docs/swagger";
				x.SwaggerEndpoint($"/swagger/{_docsVersion}/swagger.json", _titleApi);
			});
			app.UseReDoc(x =>
			{
				x.RoutePrefix = "docs";
				x.SpecUrl($"/swagger/{_docsVersion}/swagger.json");
				x.DocumentTitle = _titleApi;
			});
		}
		
		app.UseMiddleware<RefreshTokenMiddleware>();
		app.UseAuthentication();

		app.UseRouting();
		app.UseCors(_corsPolicy);
		app.UseAuthorization();

		return app;
	}

	public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
	{
		var options = new T();
		configuration.GetSection(sectionName).Bind(options);

		return options;
	}
}
