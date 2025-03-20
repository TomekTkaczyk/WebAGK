using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;

internal class ConfirmEmailTokenCreator : IConfirmEmailTokenCreator
{
	private readonly string _templatePath = Path.Combine(Directory.GetCurrentDirectory(), "templates", "EmailConfirmationTemplate.html");

	public async Task<string> GenerateEmailBodyAsync(string email, string token)
	{
		var _confirmUrl = $"http://localhost:5000/users-module/EmailMessage/confirm?EmailMessage={email}&ConfirmToken={token}";
		var _template = await File.ReadAllTextAsync(_templatePath);

		return _template.Replace("{{ConfirmUrl}}", _confirmUrl);
	}
}
