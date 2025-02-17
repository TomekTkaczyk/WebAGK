using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAGK.Module.Users.Core.DTO;
using WebAGK.Module.Users.Core.Services;
using WebAGK.Shared.Abstractions.Contexts;

namespace WebAGK.Module.Users.Api.Controllers;


[Route(UserModule.BasePath + "/[controller]")]
internal class EmailController(IEmailVerificationService service) : HomeControllerBase
{
	[HttpPost("send")]
	[AllowAnonymous]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<ActionResult> EmailSendAsync(CancellationToken cancellationToken)
	{
		await service.SendSample(cancellationToken);

		return Ok();
	}


}
