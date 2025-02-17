using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;

internal class ConfirmEmailTokenCreator : IConfirmEmailTokenCreator
{
	private readonly string _templatePath = Path.Combine(Directory.GetCurrentDirectory(), "templates", "EmailConfirmationTemplate.html");

	public async Task<string> GenerateEmailBodyAsync(string email, string token)
	{
		string confirmUrl = $"http://localhost:5000/users-module/Email/confirm?Email={email}&ConfirmToken={token}";
		string template = await File.ReadAllTextAsync(_templatePath);

		return template.Replace("{{ConfirmUrl}}", confirmUrl);
	}
}
