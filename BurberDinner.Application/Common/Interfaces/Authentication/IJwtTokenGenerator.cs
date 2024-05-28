
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Common.Interfaces.Authentication
{
  public interface IJwtTokenGenerator
  {
    string GenerateToken(User user);
  }
}