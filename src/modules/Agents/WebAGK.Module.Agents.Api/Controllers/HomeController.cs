using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Agents.Api.Controllers;

[Route(AgentModule.BasePath)]
[Authorize(Roles ="Admin")]
internal class HomeController() : HomeControllerBase
{

}
