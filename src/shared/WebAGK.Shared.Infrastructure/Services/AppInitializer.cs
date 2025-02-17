using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAGK.Shared.Infrastructure.Services;
internal class AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger) : IHostedService
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;
	private readonly ILogger<AppInitializer> _logger = logger;

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(x => x.GetTypes())
			.Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

		using(var scope = _serviceProvider.CreateScope()) {
			foreach(var dbType in dbContextTypes) {
				if(scope.ServiceProvider.GetService(dbType) is not DbContext dbContext) {
					continue;
				}
				await dbContext.Database.MigrateAsync(cancellationToken);
				_logger.LogInformation("Database migration {DbContext}", dbContext.GetType().Name.Replace("DbContext", ""));
			}
		}

		CreateTemplatesDirectory();
		CreateEmailConfirmTokenTemplate();
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

	private static void CreateTemplatesDirectory()
	{
		var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
		if(!Directory.Exists(path)) {
			Directory.CreateDirectory(path);
		}
	}

	private static void CreateEmailConfirmTokenTemplate()
	{
		var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "EmailConfirmTokenTemplate.html");
		if(!File.Exists(path)) {
			if(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailConfirmTokenTemplate.html"))) {
				File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailConfirmTokenTemplate.html"), path);
			}
			else {
				File.Create(path);
				File.WriteAllText(path, @"<!DOCTYPE html>
											<html lang='en'>
											<head>
												<meta charset='UTF-8'>
												<meta name='viewport' content='width=device-width, initial-scale=1.0'>
												<title>Confirm Your Email</title>
											</head>
											<body>
												<h1>Confirm Your Email</h1>
												<p>Click the link below to confirm your email:</p>
												<a href='{{ConfirmUrl}}'>Confirm Email</a>
											</body>
											</html>");
			}
		}
	}
}
