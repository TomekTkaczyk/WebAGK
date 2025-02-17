using WebAGK.Shared.Abstractions.Modules;
using System.Reflection;

namespace WebAGK.Bootstraper;

internal static class ModuleLoader
{
	public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
	{
		const string modulePart = "WebAGK.Module.";

		var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
		var location =	assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
		var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll*")
			.Where(x => !location.Contains(x, StringComparer.InvariantCultureIgnoreCase))
			.ToList();
		files.Sort();

		var disableModules = new List<string>();
		foreach(var file in files) {
			if(!file.Contains(modulePart)) {
				continue;
			}
			// Get module name e.g. Employyes
			var moduleName = file.Split(modulePart)[1].Split(".")[0];
			var enabled = configuration.GetValue<bool>($"modules:{moduleName}:enabled");
			if(!enabled) {
				disableModules.Add(file);
			}
		}

		foreach(var disabledModule in disableModules) {
			files.Remove(disabledModule);
		}

		files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

		return assemblies;
	}

	public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
		=> assemblies.SelectMany(x => x.GetTypes())
		.Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
		.OrderBy(x => x.Name)
		.Select(Activator.CreateInstance)
		.Cast<IModule>()
		.ToList();

}
