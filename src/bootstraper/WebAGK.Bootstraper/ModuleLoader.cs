using WebAGK.Shared.Abstractions.Modules;
using System.Reflection;

namespace WebAGK.Bootstraper;

internal static class ModuleLoader
{
	public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
	{
		const string _modulePart = "WebAGK.Module.";

		var _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
		var _location =	_assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
		var _files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll*")
			.Where(x => !_location.Contains(x, StringComparer.InvariantCultureIgnoreCase))
			.ToList();
		_files.Sort();

		var _disableModules = new List<string>();
		foreach(var _file in _files) {
			if(!_file.Contains(_modulePart)) {
				continue;
			}
			// Get module name e.g. Employyes
			var _moduleName = _file.Split(_modulePart)[1].Split(".")[0];
			var _enabled = configuration.GetValue<bool>($"modules:{_moduleName}:enabled");
			if(!_enabled) {
				_disableModules.Add(_file);
			}
		}

		foreach(var _disabledModule in _disableModules) {
			_files.Remove(_disabledModule);
		}

		_files.ForEach(x => _assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

		return _assemblies;
	}

	public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
		=> assemblies.SelectMany(x => x.GetTypes())
		.Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
		.OrderBy(x => x.Name)
		.Select(Activator.CreateInstance)
		.Cast<IModule>()
		.ToList();

}
