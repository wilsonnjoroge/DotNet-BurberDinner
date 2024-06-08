using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.MenuAggregate.ValueObjects;
using BurberDinner.Domain.MenuReviewAggregate.ValueObjects;
using BurberDinner.Domain.HostAggregate.ValueObjects;
using System;
using System.Collections.Generic;

namespace BurberDinner.Domain.MenuAggregate.Entities
{
    public sealed class Menu : Entity<MenuId>
    {
        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviewIds = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double? AverageRating { get; private set; } = null;
        public HostId HostId { get; private set; }
        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }


        // Parameterized constructor
        private Menu(
            MenuId menuId,
            HostId hostId,
            string name,
            string description,
            double? averageRating,
            DateTime createdDateTime,
            DateTime updatedDateTime,
            List<MenuSection> sections) : base(menuId)
        {
            HostId = hostId;
            Name = name;
            Description = description;
            AverageRating = averageRating;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
            _sections = sections;
        }

        // Factory method to create a new Menu instance
        public static Menu Create(
            HostId hostId,
            string name,
            string description,
            double? averageRating,
            List<MenuSection> sections)
        {
            return new Menu(
                MenuId.CreateUnique(),
                hostId,
                name,
                description,
                averageRating ?? 0,
                DateTime.UtcNow,
                DateTime.UtcNow,
                sections);
        }

        // Method to add a dinner to the menu
        public void AddDinner(DinnerId dinnerId)
        {
            if (!_dinnerIds.Contains(dinnerId))
            {
                _dinnerIds.Add(dinnerId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to remove a dinner from the menu
        public void RemoveDinner(DinnerId dinnerId)
        {
            if (_dinnerIds.Contains(dinnerId))
            {
                _dinnerIds.Remove(dinnerId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to add a menu review to the menu
        public void AddMenuReview(MenuReviewId menuReviewId)
        {
            if (!_menuReviewIds.Contains(menuReviewId))
            {
                _menuReviewIds.Add(menuReviewId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to remove a menu review from the menu
        public void RemoveMenuReview(MenuReviewId menuReviewId)
        {
            if (_menuReviewIds.Contains(menuReviewId))
            {
                _menuReviewIds.Remove(menuReviewId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to add a section to the menu
        public void AddSection(MenuSection section)
        {
            _sections.Add(section);
            UpdatedDateTime = DateTime.UtcNow;
        }

        // Method to remove a section from the menu
        public void RemoveSection(MenuSectionId sectionId)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section is not null)
            {
                _sections.Remove(section);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to update a section in the menu
        public void UpdateSection(MenuSection section)
        {
            var existingSection = _sections.Find(s => s.Id == section.Id);
            if (existingSection is not null)
            {
                _sections.Remove(existingSection);
                _sections.Add(section);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to add a menu item to a section in the menu
        public void AddMenuItemToSection(MenuSectionId sectionId, MenuItem item)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section is not null)
            {
                section.AddItem(item);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to remove a menu item from a section in the menu
        public void RemoveMenuItemFromSection(MenuSectionId sectionId, MenuItemId itemId)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section is not null)
            {
                section.RemoveItem(itemId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Method to update a menu item in a section in the menu
        public void UpdateMenuItemInSection(MenuSectionId sectionId, MenuItem item)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section is not null)
            {
                section.UpdateItem(item);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        // Additional methods can be added as needed

    }
}
