
using BurberDinner.Application.Common.Interfaces.Persistence;
using BurberDinner.Domain.MenuAggregate.Entities;

namespace BurberDinner.Infrastructure.Persistence
{
    public class MenuRepository : IMenuRepository
    {
      private static readonly List<Menu> _menus = new();
        public void Add(Menu menu)
        {
          _menus.Add(menu);
        }
    }
}