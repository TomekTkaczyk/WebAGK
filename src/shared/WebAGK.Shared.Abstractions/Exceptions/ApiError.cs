namespace WebAGK.Shared.Abstractions.Exceptions;
public class ApiError
{
	private readonly List<ValidationError> _validationErrors = [];

	public string Message { get; set; }
	public int Status { get; set; }
	public string Code { get; set; }
	public string Title { get; set; }
	public string Type { get; set; }
	public string Detail { get; set; }
	public string Instance { get; set; }

	public IReadOnlyList<ValidationError> ValidationErrors => _validationErrors;

	public void AddValidationError(string field, string code, string message)
	{
		_validationErrors.Add(new ValidationError(field, code, message));
	}
}
