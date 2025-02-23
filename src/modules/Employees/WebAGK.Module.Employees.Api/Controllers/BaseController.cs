using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Employees.Api.Controllers;

[ApiController]
[Route(EmployeeModule.BasePath+"/[controller]")]
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
