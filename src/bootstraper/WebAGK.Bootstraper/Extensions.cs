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
		var pathCombine = Path.Combine(Directory.GetCurrentDirectory(), path);

		if(!Directory.Exists(pathCombine)) {
			Directory.CreateDirectory(pathCombine);
		}
	}
	private static void EnsureFileExists(string path, string fileName, string template)
	{
		var pathCombine = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);

		if(!File.Exists(pathCombine)) {
			File.Create(pathCombine);
			File.WriteAllText(pathCombine, template);
		}
	}

	private static string TokenConfirmEmailTemplate()
	{
		return @"

			";
	}
}
