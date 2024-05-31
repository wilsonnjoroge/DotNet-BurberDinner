
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Authentication.Common
{
  public record AuthenticationResult
  (
    User User,
    string Token
  );
}