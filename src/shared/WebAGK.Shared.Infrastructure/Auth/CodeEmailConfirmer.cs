using WebAGK.Shared.Abstractions.Auth;

namespace WebAGK.Shared.Infrastructure.Auth;
internal class CodeEmailConfirmer() : IEmailConfirmer
{
	public string ConfirmToken { get; private set; }

	public string GetRemindPasswordBody(Guid userId, string email)
	{
		GenerateVerificationCode();

		return $"Confirm code: {ConfirmToken}";
	}

	public string GetConfirmEmailBody(Guid userId, string email)
	{
		GenerateVerificationCode();

		return $"Confirm code: {ConfirmToken}";
	}

	private void GenerateVerificationCode()
	{
		var rnd = new Random();
		ConfirmToken = rnd.Next(100000, 999999).ToString();
	}

	public bool Confirm(string expected, string received, string email) => expected.Equals(received, StringComparison.InvariantCultureIgnoreCase);

	public string GetConfirmToken(Guid userId, string email)
	{
		return ConfirmToken;
	}

	public string GetConfirmEmailBody(Guid userId, string email, string confirmUrl)
	{
		throw new NotImplementedException();
	}
}
