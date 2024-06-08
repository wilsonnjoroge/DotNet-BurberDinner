using BurberDinner.Application.Common.Errors; 
using Microsoft.AspNetCore.Diagnostics; 
using Microsoft.AspNetCore.Mvc; 

namespace BurberDinner.Api.Controllers
{
  // Controller to handle error responses
  public class ErrorsController : ControllerBase
  {
    // Action method to handle errors and return appropriate responses
    [Route("/error")]
    public IActionResult Error()
    {
      // Retrieve the exception from the HTTP context
      Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

      // Determine the status code and message based on the type of exception
      var (statusCode, message) = exception switch
      {
        // Handle specific error types with custom messages
        DuplicateEmailException => (StatusCodes.Status409Conflict, "Email already exists"),
        _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
      };

      // Return a problem response with the determined status code and message
      return Problem(statusCode: statusCode, title: message);
    }
  }
}
