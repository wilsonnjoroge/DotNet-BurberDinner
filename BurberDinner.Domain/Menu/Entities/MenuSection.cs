
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Menu.ValueObjects;

namespace BurberDinner.Domain.Menu.Entities
{
  public sealed class MenuSection : Entity<MenuSectionId>
  {
    private readonly List<MenuItems> _items = new ();
    public string Name { get; }
    public string Description { get; }
  }
}