using Microsoft.Extensions.Options;

namespace WebAGK.Shared.Infrastructure.Services;

internal class SmtpOptionsValidator : IValidateOptions<SmtpOptions>
{
    public ValidateOptionsResult Validate(string name, SmtpOptions options)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(options.Host))
        {
            errors.Add("SMTP Host is required.");
        }

        if (options.Port <= 0)
        {
            errors.Add("SMTP Port must be a positive number.");
        }

        if (string.IsNullOrWhiteSpace(options.Account))
        {
            errors.Add("SMTP Account is required.");
        }

        if (string.IsNullOrWhiteSpace(options.Password))
        {
            errors.Add("SMTP Password is required.");
        }

        if (string.IsNullOrWhiteSpace(options.Issuer))
        {
            errors.Add("SMTP Issuer is required.");
        }

        if (string.IsNullOrWhiteSpace(options.IssuerEmail))
        {
            errors.Add("SMTP IssuerEmail is required.");
        }

        return errors.Count != 0
            ? ValidateOptionsResult.Fail(string.Join("; ", errors))
            : ValidateOptionsResult.Success;
    }
}
