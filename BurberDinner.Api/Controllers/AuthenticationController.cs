using BurberDinner.Application.Authentication.Commands.Register; 
using BurberDinner.Application.Authentication.Common; 
using BurberDinner.Application.Authentication.Queries.Login; 
using BurberDinner.Contracts.Authentication; 
using BurberDinner.Domain.Common.Errors; 
using ErrorOr;
using MapsterMapper; 
using MediatR; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace BurberDinner.Api.Controllers
{
    // Define a controller for authentication-related endpoints
    [Route("auth")]
    [AllowAnonymous] 
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator; // Mediator for sending requests and commands
        private readonly IMapper _mapper; // Mapper for object mapping

        // Constructor to inject dependencies
        public AuthenticationController(ISender mediator, IMapper mapper) 
        {
            _mediator = mediator; // Assign the mediator
            _mapper = mapper; // Assign the mapper
        }

        // Endpoint for user registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // Map the request to the corresponding command
            var command = _mapper.Map<RegisterCommand>(request);
            
            // Send the register command to the mediator and get the result
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            // Map the response to the appropriate ActionResult
            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)), // Return success
                errors => Problem(errors) // Return errors
            );
        }

        // Endpoint for user login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Map the request to the corresponding query
            var query = _mapper.Map<LoginQuery>(request);
            
            // Send the login query to the mediator and get the result
            var authResult = await _mediator.Send(query);

            // Check if there was an error and handle it
            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                // Return a 401 Unauthorized response with the error message
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authResult.FirstError.Description
                );
            }

            // Map the response to the appropriate ActionResult
            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)), // Return success
                errors => Problem(errors) // Return errors
            );
        }
    }
}

