
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Services.Authentication.Common
{
  public record AuthenticationResult
  (
    User user,
    string Token
  );
}