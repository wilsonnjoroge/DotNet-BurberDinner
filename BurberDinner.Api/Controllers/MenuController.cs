
using BurberDinner.Contracts.Menus;
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
  [Route("hosts/{hostId}/menus")]
  public class MenuController : ApiController
  {
    [HttpPost]
    public IActionResult CreateMenu(CreateMenuRequest request, string hostId)
    {
      return Ok();
    }
  }
}