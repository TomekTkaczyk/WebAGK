namespace WebAGK.Bootstraper;

internal static class Extensions
{
	internal static void EnsureDirectoryExists()
	{
		EnsureDirectoryExists("templates");
		EnsureFileExists("templates", "EmailConfirmationTemplate.html", TokenConfirmEmailTemplate());
	}

	private static void EnsureDirectoryExists(string path)
	{
		var _pathCombine = Path.Combine(Directory.GetCurrentDirectory(), path);

		if(!Directory.Exists(_pathCombine)) {
			Directory.CreateDirectory(_pathCombine);
		}
	}
	private static void EnsureFileExists(string path, string fileName, string template)
	{
		var _pathCombine = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

		if(!File.Exists(_pathCombine)) {
			File.Create(_pathCombine);
			File.WriteAllText(_pathCombine, template);
		}
	}

	private static string TokenConfirmEmailTemplate()
	{
		return @"

			";
	}
}
