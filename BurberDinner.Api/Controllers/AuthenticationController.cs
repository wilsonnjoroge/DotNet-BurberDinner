
using BurberDinner.Application.Services.Authentication;
using BurberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BurberDinner.Api.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthenticationController : ControllerBase
  {

    private readonly  IAuthenticationService _authenticationService;
    public AuthenticationController(IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
      // Use the injected IAuthenticationService to obtain register method
      var authResult = _authenticationService.Register(
        request.FirstName, 
        request.LastName, 
        request.Email, 
        request.Password
        );

      // Map the response to the authentication result defined
        var response = new AuthenticationResponse
        (
          authResult.user.Id,
          authResult.user.FirstName,
          authResult.user.LastName,
          authResult.user.Email,
          authResult.Token
        );
      // Return the response
      return Ok(response);
    }


    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
      // Use the injected IAuthenticationService to obtain login method
      var authResult = _authenticationService.Login(
        request.Email, 
        request.Password
        );

      // Map the response to the authentication result defined
        var response = new AuthenticationResponse
        (
          authResult.user.Id,
          authResult.user.FirstName,
          authResult.user.LastName,
          authResult.user.Email,
          authResult.Token
        );
      // Return the response
      return Ok(response);
    }

  }
}

