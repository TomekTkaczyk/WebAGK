namespace WebAGK.Shared.Abstractions.Contexts;
public interface IContext
{
	string Request { get; }
	string TraceId { get; }
	IIdentityContext Identity { get; }
}
