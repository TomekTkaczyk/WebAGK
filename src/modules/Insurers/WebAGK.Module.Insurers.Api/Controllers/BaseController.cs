using Microsoft.AspNetCore.Mvc;

namespace WebAGK.Module.Insurers.Api.Controllers;

[ApiController]
[Route(InsurerModule.BasePath+"/[controller]")]
public abstract class BaseController : ControllerBase {
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if(model is null) {
            return NotFound();
        }

        return Ok(model);
    }
}