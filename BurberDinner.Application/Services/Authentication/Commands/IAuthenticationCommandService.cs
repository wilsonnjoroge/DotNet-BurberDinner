
using BurberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BurberDinner.Application.Services.Authentication.Commands
{
  public interface IAuthenticationCommandService
  {
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);

  }
}