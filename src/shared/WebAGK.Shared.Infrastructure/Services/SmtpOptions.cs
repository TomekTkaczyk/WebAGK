namespace WebAGK.Shared.Infrastructure.Services;

public sealed class SmtpOptions
{
    public string Host { get; set; }
    public string Issuer { get; set; }
    public string IssuerEmail { get; set; }
    public int Port { get; set; }
    public bool DefaultCredentials { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }
    public bool Ssl { get; set; }
}
