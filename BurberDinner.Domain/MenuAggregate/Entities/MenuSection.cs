using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Menu.Entities;
using BurberDinner.Domain.Menu.ValueObjects;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new();
    public string Name { get; }
    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public MenuSection(MenuSectionId menuSectionId, string name, string description) : base(menuSectionId)
    {
        Name = name;
        Description = description;
    }

    public static MenuSection Create(string name, string description)
    {
        return new MenuSection(
            MenuSectionId.CreateUnique(),
            name,
            description);
    }

    public void AddItem(MenuItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(MenuItemId itemId)
    {
        var item = _items.Find(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void UpdateItem(MenuItem item)
    {
        var existingItem = _items.Find(i => i.Id == item.Id);
        if (existingItem != null)
        {
            _items.Remove(existingItem);
            _items.Add(item);
        }
    }
}
