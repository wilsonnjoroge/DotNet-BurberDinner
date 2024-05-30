using BurberDinner.Application.Authentication.Commands.Register;
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Authentication.Queries.Login;
using BurberDinner.Contracts.Authentication;
using BurberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
      private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
      public async Task <IActionResult> Register(RegisterRequest request)
      {
          // Use the injected IAuthenticationService to call the register method
          var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
          ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

          // Map the response to the authentication result defined
          return authResult.Match(
              authResult => Ok(MapToAuthenticationResponse(authResult)),
              errors => Problem(errors)
          );
      }

        [HttpPost("login")]
        public async Task <IActionResult> Login(LoginRequest request)
        {
          var query = new LoginQuery(request.Email, request.Password);
            // Use the injected IAuthenticationService to call the login method
            var authResult = await _mediator.Send(query);

            if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
              return Problem(
                statusCode : StatusCodes.Status401Unauthorized,
                title : authResult.FirstError.Description
              );
            }

            // Map the response to the authentication result defined
            return authResult.Match(
                authResult => Ok(MapToAuthenticationResponse(authResult)),
                errors => Problem(errors)
            );
        }

        private static AuthenticationResponse MapToAuthenticationResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse
            (
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token
            );
        }
    }
}
