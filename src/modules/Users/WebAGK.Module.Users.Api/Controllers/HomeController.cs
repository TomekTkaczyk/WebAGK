using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Users.Api.Controllers;

[Route(UserModule.BasePath)]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() =>"UsersTests API";
}