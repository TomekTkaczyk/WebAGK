using System.Reflection;

namespace AGK.Application;

public static class AssemblyReference
{
	public static readonly Assembly ApplicationAssembly = typeof(AssemblyReference).Assembly;
}