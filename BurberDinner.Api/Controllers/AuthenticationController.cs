using BurberDinner.Application.Authentication.Commands.Register;
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Authentication.Queries.Login;
using BurberDinner.Contracts.Authentication;
using BurberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
      private readonly ISender _mediator;
      private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
      public async Task <IActionResult> Register(RegisterRequest request)
      {
          // Use the injected IAuthenticationService to call the register method
          var command = _mapper.Map<RegisterCommand>(request);
          ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

          // Map the response to the authentication result defined
          return authResult.Match(
              authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
              errors => Problem(errors)
          );
      }

        [HttpPost("login")]
        public async Task <IActionResult> Login(LoginRequest request)
        {
          var query = _mapper.Map<LoginQuery>(request);
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
                authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
                errors => Problem(errors)
            );
        }
    }
}
