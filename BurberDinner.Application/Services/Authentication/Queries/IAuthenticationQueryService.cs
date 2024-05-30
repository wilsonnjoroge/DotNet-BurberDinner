
using BurberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BurberDinner.Application.Services.Authentication
{
  public interface IAuthenticationQueryService
  {
    ErrorOr<AuthenticationResult> Login(string email, string password);

  }
}