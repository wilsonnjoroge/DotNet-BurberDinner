
using BurberDinner.Domain.Entities;

namespace BurberDinner.Application.Common.Interfaces.Persistence
{
  public interface IUserRepository
  {
    User? GetUserByEmail(string email);
    void AddUser(User user);
  }
}