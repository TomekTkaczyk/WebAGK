using System.Reflection;

namespace AGK.DataAccess;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}