
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.MenuAggregate.ValueObjects;


namespace BurberDinner.Domain.MenuAggregate.Entities
{
  public sealed class MenuItem : Entity<MenuItemId>
  {
    private readonly List<MenuItem> _items = new ();

    private MenuItem(MenuItemId menuItemId, string name, string description) : base(menuItemId)
    {
      Name = name;
      Description = description;
    }

    public static MenuItem Create(MenuItemId menuItemId, string name, string description)
    {
      return new (MenuItemId.CreateUnique(),name, description);
    }

    public string Name { get; }
    public string Description { get; }
  }
}