using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAGK.Shared.Infrastructure.Services;
internal class AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger) : IHostedService
{
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		CreateTemplatesDirectory(logger);
		CreateEmailConfirmTokenTemplate(logger);
		await Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

	private static void CreateTemplatesDirectory(ILogger<AppInitializer> logger)
	{
		var _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
		
		if (Directory.Exists(_path)) return;
		
		logger.LogInformation("Create templates directory.");
		Directory.CreateDirectory(_path);
	}

	private static void CreateEmailConfirmTokenTemplate(ILogger<AppInitializer> logger)
	{
		var _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmailConfirmTokenTemplate.html");
		
		if (File.Exists(_path)) return;
		
		if(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailConfirmTokenTemplate.html"))) {
			logger.LogInformation("Copy email confirm message from template.");
			File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailConfirmTokenTemplate.html"), _path);
		} else {
			logger.LogInformation("Create template of email confirm token template.");
			File.Create(_path);
			File.WriteAllText(_path, """
			                         <!DOCTYPE html>
			                         	<html lang='en'>
			                         	<head>
			                         		<meta charset='UTF-8'>
			                         		<meta name='viewport' content='width=device-width, initial-scale=1.0'>
			                         		<title>Confirm Your EmailMessage</title>
			                         	</head>
			                         	<body>
			                         		<h1>Confirm Your EmailMessage</h1>
			                         		<p>Click the link below to confirm your email:</p>
			                         		<a href='{{ConfirmUrl}}'>Confirm EmailMessage</a>
			                         	</body>
			                         	</html>
			                         """);
		}
	}
}
