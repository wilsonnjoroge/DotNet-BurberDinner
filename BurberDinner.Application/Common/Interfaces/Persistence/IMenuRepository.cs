
using BurberDinner.Domain.MenuAggregate.Entities;

namespace BurberDinner.Application.Common.Interfaces.Persistence
{
  public interface IMenuRepository
  {
    void Add(Menu menu);
  }
}