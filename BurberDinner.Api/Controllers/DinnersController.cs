
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
  [Route("[controller]")]
  public class DinnersController : ApiController
  {
    [HttpGet]
    public IActionResult ListDinner()
    {
      return Ok();
    }
  }
}