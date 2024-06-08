
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
  // Define a controller for managing dinner-related endpoints
  [Route("[controller]")]
  public class DinnersController : ApiController
  {
    // Define an HTTP GET endpoint for listing dinners
    [HttpGet]
    public IActionResult ListDinner()
    {
      // Return a 200 OK response
      return Ok();
    }
  }
}
