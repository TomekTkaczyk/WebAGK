
using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Employees.Api.Controllers;

[Route(EmployeeModule.BasePath)]
internal class HomeController : HomeControllerBase
{
	[HttpGet]
	public ActionResult<string> Get() =>"Employees API";
}
