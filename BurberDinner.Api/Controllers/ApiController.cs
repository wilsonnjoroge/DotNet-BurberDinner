
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurberDinner.Api.Controllers
{
  [ApiController]
  [Authorize]
  public class ApiController : ControllerBase
  {
    protected IActionResult Problem(List<Error> errors)
    {
      if (errors.All(error => error.Type == ErrorType.Validation))
      {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
          modelStateDictionary.AddModelError(
            error.Code, 
            error.Description);
        }

        return ValidationProblem(modelStateDictionary);
      };

      var firstError = errors[0];

      var statusCode = firstError.Type switch
      {
          ErrorType.Conflict => StatusCodes.Status409Conflict,
          ErrorType.Validation => StatusCodes.Status401Unauthorized,
          ErrorType.NotFound => StatusCodes.Status500InternalServerError,
          // ErrorType.Failure => throw new NotImplementedException(),
          // ErrorType.Unexpected => throw new NotImplementedException(),
          // ErrorType.Unauthorized => throw new NotImplementedException(),
          // ErrorType.Forbidden => throw new NotImplementedException(),
          // _ => throw new NotImplementedException()
      };

      return Problem(statusCode : statusCode, title: firstError.Description);
    }
  }
}