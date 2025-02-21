using WebAGK.Shared.Infrastructure;
using WebAGK.Shared.Infrastructure.Modules;
using WebAGK.Shared.Infrastructure.Services;

namespace WebAGK.Bootstraper;

public static class Program
{
	public static void Main(string[] args)
	{
		var _builder = WebApplication.CreateBuilder(args);
		
		var _configuration = _builder.Configuration;
		var _services = _builder.Services;

		_builder.Host.ConfigureModules();

		var _assemblies = ModuleLoader.LoadAssemblies(_configuration);
		var _modules = ModuleLoader.LoadModules(_assemblies);

		_builder.Services.AddInfrastructure(_configuration, _modules);

		foreach(var _module in _modules) {
			_module.Register(_services, _configuration);
		}

		_builder.Services.AddControllers();
		_builder.Services.Configure<SmtpOptions>(_builder.Configuration.GetSection("smtp"));

		var _app = _builder.Build();

		_app.Logger.LogInformation("Modules: {Modules}", string.Join(",", _modules.Select(m => m.Name)));
		
		_app.UseInfrastructure(_app.Environment);
		foreach(var _module in _modules) {
			_module.Use(_app);
		}

		_app.MapControllers();

		_app.MapGet("/", context => context.Response.WriteAsync("WebAGK API."));
		_app.MapGet("modules", context =>
		{
			var _moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
			return context.Response.WriteAsJsonAsync(_moduleInfoProvider);
		});

		_app.MapGet("permissions", context =>
		{
			var _moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
			var _permissions = new Dictionary<string, IEnumerable<string>>();
			foreach(var _module in _moduleInfoProvider.ModuleInfos) {
				if(_permissions.TryGetValue(_module.Name, out var _value)) {
					_permissions[_module.Name] = _value.Union(_module.Policies);
				}
				else {
					_permissions.Add(_module.Name, _module.Policies);
				}
			}

			return context.Response.WriteAsJsonAsync(_permissions);
		});

		_assemblies.Clear();
		_modules.Clear();
		
		_app.Run();
	}
}
