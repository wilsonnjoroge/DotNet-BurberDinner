using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.MenuAggregate.ValueObjects;

namespace BurberDinner.Domain.MenuAggregate.Entities
{
    public sealed class MenuItem : Entity<MenuItemId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private MenuItem(
            MenuItemId id,
            string name,
            string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        // The static Create method to instantiate MenuItem
        public static MenuItem Create(string name, string description)
        {
            // Add validation logic if needed
            return new MenuItem(
                MenuItemId.CreateUnique(), // Generate a unique ID
                name,
                description
            );
        }
    }
}
