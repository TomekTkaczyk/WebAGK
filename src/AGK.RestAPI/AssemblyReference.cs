using System.Reflection;

namespace AGK.RestAPI;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}