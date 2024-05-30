using BurberDinner.Application.Authentication.Commands.Register;
using BurberDinner.Application.Services.Authentication;
using BurberDinner.Application.Services.Authentication.Commands;
using BurberDinner.Application.Services.Authentication.Common;
using BurberDinner.Contracts.Authentication;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
      private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
      public async Task <IActionResult> Register(RegisterRequest request)
      {
          // Use the injected IAuthenticationService to call the register method
          var query = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
          ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

          // Map the response to the authentication result defined
          return authResult.Match(
              authResult => Ok(MapToAuthenticationResponse(authResult)),
              errors => Problem(errors)
          );
      }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Use the injected IAuthenticationService to call the login method
            var authResult = _authenticationQueryService.Login(
                request.Email,
                request.Password
            );

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
