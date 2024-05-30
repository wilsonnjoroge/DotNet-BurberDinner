
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Authentication.Common
{
  public record AuthenticationResult
  (
    User user,
    string Token
  );
}