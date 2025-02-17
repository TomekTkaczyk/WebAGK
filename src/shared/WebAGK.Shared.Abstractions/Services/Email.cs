using System.Text.Json;

namespace WebAGK.Shared.Abstractions.Services;

public class Email
{
	public string[] Recievers { get; set; }
	public string Subject { get; set; }
	public string Body { get; set; }

	public override string ToString()
	{
		return JsonSerializer.Serialize(this);
	}
}
