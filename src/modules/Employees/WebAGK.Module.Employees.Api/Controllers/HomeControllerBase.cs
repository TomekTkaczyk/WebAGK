using Microsoft.AspNetCore.Mvc;
using WebAGK.Shared.Infrastructure.Api;

namespace WebAGK.Module.Employees.Api.Controllers;

[ApiController]
[Route(EmployeeModule.BasePath)]
[ProducesDefaultContentType]
internal abstract class HomeControllerBase : ControllerBase
{
	protected ActionResult<T> OkOrNotFound<T>(T model)
	{
		if(model is null) {
			return NotFound();
		}

		return Ok(model);
	}
}
