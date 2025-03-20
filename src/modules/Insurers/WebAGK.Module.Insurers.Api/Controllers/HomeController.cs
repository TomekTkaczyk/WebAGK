using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Insurers.Api.Controllers;

[Route(InsurerModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Insurer API";
}
