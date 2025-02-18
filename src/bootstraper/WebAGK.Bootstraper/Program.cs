using WebAGK.Shared.Abstractions.Modules;
using WebAGK.Shared.Infrastructure;
using WebAGK.Shared.Infrastructure.Modules;
using WebAGK.Shared.Infrastructure.Services;
using System.Reflection;
using WebAGK.Shared.Infrastructure.ValueObject;

namespace WebAGK.Bootstraper;

public class Program
{
	public static void Main(string[] args)
	{
		IList<Assembly> _assemblies;
		IList<IModule> _modules;

		var builder = WebApplication.CreateBuilder(args);
		
		var configuration = builder.Configuration;
		var services = builder.Services;

		builder.Host.ConfigureModules();

		_assemblies = ModuleLoader.LoadAssemblies(configuration);
		_modules = ModuleLoader.LoadModules(_assemblies);

		builder.Services.AddInfrastructure(configuration, _modules);

		foreach(var module in _modules) {
			module.Register(services, configuration);
		}

		builder.Services.AddControllers();
		builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("smtp"));


		var app = builder.Build();

		app.Logger.LogInformation("Modules: {Modules}", string.Join(",", _modules.Select(m => m.Name)));

		app.UseInfrastructure(app.Environment);
		foreach(var module in _modules) {
			module.Use(app);
		}

		app.MapControllers();

		app.MapGet("/", context => context.Response.WriteAsync("WebAGK API."));
		app.MapGet("modules", context =>
		{
			var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
			return context.Response.WriteAsJsonAsync(moduleInfoProvider);
		});

		app.MapGet("permissions", context =>
		{
			var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
			IDictionary<string, IEnumerable<string>> permissions = new Dictionary<string, IEnumerable<string>>();
			foreach(var module in moduleInfoProvider.MolueInfos) {
				foreach(var policy in module.Policies) {
					if(permissions.ContainsKey(module.Name)) {
						permissions[module.Name] = permissions[module.Name].Union(module.Policies);
					}
					else {
						permissions.Add(module.Name, module.Policies);
					}
				}
			}

			return context.Response.WriteAsJsonAsync(permissions);
		});

		//app.MapGet("email", context =>
		//{
		//	var confirmer = context.RequestServices
		//		.GetRequiredService<EmailConfirmerFactory>()
		//		.GetEmailConfirmer();
		//	var body = confirmer.GetConfirmEmailBody(Guid.NewGuid(), "biuro@unipromax.pl");
		//	var email = new EmailMessage
		//	{
		//		Body = body,
		//		Subject = "Sample activating your account in the WebAGK application",
		//		Recievers = ["biuro@unipromax.pl"]
		//	};
		//	EmailsQueue.Add(email);
		//	return context.Response.WriteAsync("WebAGK API.");
		//});

		_assemblies.Clear();
		_modules.Clear();
		
		app.Run();
	}
}
