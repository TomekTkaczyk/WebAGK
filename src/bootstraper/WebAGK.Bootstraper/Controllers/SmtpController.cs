using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAGK.Shared.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace WebAGK.Bootstraper.Controllers;

[ApiController]
[Route("[controller]")]
public class SmtpController(IOptionsMonitor<SmtpOptions> optionsMonitor) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var _optionsoptions = optionsMonitor.CurrentValue;
		await Task.CompletedTask;

		return Ok(JsonSerializer.Serialize(_optionsoptions));
	}
}
