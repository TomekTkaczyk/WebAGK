namespace AGK.Application.Reponse;

public sealed record Error(string Message)
{
	public static Error None => new(string.Empty);
}
