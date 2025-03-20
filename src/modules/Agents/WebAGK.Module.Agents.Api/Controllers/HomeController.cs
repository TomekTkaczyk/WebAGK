using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Agents.Api.Controllers;

[Route(AgentModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => "Agents API";
}
