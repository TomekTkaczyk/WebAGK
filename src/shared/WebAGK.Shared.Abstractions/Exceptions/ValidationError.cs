namespace WebAGK.Shared.Abstractions.Exceptions;
public record ValidationError(
	string Field,
	string Code,
	string Message);
