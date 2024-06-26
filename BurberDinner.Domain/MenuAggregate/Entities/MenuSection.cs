using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.MenuAggregate.ValueObjects;

namespace BurberDinner.Domain.MenuAggregate.Entities
{
    public sealed class MenuSection : Entity<MenuSectionId>
    {
        private readonly List<MenuItem> _items = new();

        public string Name { get; }
        public string Description { get; }

        public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

        public MenuSection(MenuSectionId menuSectionId, string name, string description, List<MenuItem> items = null!) : base(menuSectionId)
        {
            Name = name;
            Description = description;
            _items = items ?? new List<MenuItem>(); // Initialize with provided items or empty list
        }

        public static MenuSection Create(string name, string description, List<MenuItem> items)
        {
            return new MenuSection(
                MenuSectionId.CreateUnique(),
                name,
                description,
                items
            );
        }

        public void AddItem(MenuItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(MenuItemId itemId)
        {
            var item = _items.Find(i => i.Id == itemId);
            if (item is not null)
            {
                _items.Remove(item);
            }
        }

        public void UpdateItem(MenuItem item)
        {
            var existingItem = _items.Find(i => i.Id == item.Id);
            if (existingItem is not null)
            {
                _items.Remove(existingItem);
                _items.Add(item);
            }
        }
    }
}
