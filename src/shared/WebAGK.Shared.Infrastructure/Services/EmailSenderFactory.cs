using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAGK.Shared.Abstractions.Services;

namespace WebAGK.Shared.Infrastructure.Services;
internal class EmailSenderFactory : IEmailSenderFactory
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IConfiguration _configuration;

	public EmailSenderFactory(IServiceProvider serviceProvider, IConfiguration configuration)
	{
		_serviceProvider = serviceProvider;
		_configuration = configuration;
		RetryCountLimit = _configuration.GetValue<int>("EmailSettings:RetryCountLimit");
	}

	public int RetryCountLimit { get; private set; }

	public IEmailSender GetEmailSender()
	{
		var senderType = _configuration["EmailSettings:SenderType"];
		return senderType switch
		{
			"smtp" => _serviceProvider.GetService<SmtpEmailSender>(),
			_ => _serviceProvider.GetService<FakeEmailSender>(),
		};
	}
}
