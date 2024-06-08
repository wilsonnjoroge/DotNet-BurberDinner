
using ErrorOr; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.ModelBinding; 

namespace BurberDinner.Api.Controllers
{
    [ApiController] // Define as an API controller
    public class ApiController : ControllerBase
    {
        // Custom method to handle errors and return appropriate responses
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new ModelStateDictionary();

                // Add each validation error to the model state dictionary
                foreach (var error in errors)
                {
                    modelStateDictionary.AddModelError(error.Code, error.Description);
                }

                // Return a validation problem response
                return ValidationProblem(modelStateDictionary);
            };

            // Get the first error from the list
            var firstError = errors[0];

            // Determine the status code based on the error type
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

            // Return a problem response with the appropriate status code and error message
            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
