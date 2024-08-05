using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WakeController : ApiController
{
    [HttpGet]
    public IActionResult Wake()
    {
        return Ok();
    }
}