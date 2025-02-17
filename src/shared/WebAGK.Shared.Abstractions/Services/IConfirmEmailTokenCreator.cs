namespace WebAGK.Shared.Abstractions.Services;

public interface IConfirmEmailTokenCreator
{
	Task<string> GenerateEmailBodyAsync(string email, string token);
}