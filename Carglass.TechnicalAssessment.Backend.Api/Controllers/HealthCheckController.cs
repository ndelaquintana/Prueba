using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
[Route("KeepAlive")]
public class HealthCheckController : ControllerBase
{

    [HttpGet]
    public ActionResult KeepAlive()
    {
        return Ok();
    }
}