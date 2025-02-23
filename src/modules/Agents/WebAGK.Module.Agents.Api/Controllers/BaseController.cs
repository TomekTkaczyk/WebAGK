using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Agents.Api.Controllers;

[ApiController]
[Route(AgentModule.BasePath+"/[controller]")]
internal abstract class BaseController : ControllerBase
{
	protected ActionResult<T> OkOrNotFound<T>(T model)
	{
		if(model is null) {
			return NotFound();
		}

		return Ok(model);
	}
}
