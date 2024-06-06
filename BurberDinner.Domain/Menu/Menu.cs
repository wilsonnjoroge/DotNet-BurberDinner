using System;
using System.Collections.Generic;
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Menu.ValueObjects;
using BurberDinner.Domain.Menu.Entities;
using BurberDinner.Domain.Dinner.ValueObjects;
using BurberDinner.Domain.MenuReview.ValueObjects;

namespace BurberDinner.Domain.Menu
{
    public sealed class Menu : Entity<MenuId>
    {
        private readonly List<MenuSection> _sections = new();
        private readonly List<DinnerId> _dinnerIds = new();
        private readonly List<MenuReviewId> _menuReviewIds = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double AverageRating { get; private set; }
        public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Menu(
            MenuId menuId,
            string name,
            string description,
            double averageRating,
            DateTime createdDateTime,
            DateTime updatedDateTime) : base(menuId)
        {
            Name = name;
            Description = description;
            AverageRating = averageRating;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Menu Create(
            string name,
            string description,
            double averageRating)
        {
            return new Menu(
                MenuId.CreateUnique(),
                name,
                description,
                averageRating,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void AddDinner(DinnerId dinnerId)
        {
            if (!_dinnerIds.Contains(dinnerId))
            {
                _dinnerIds.Add(dinnerId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void RemoveDinner(DinnerId dinnerId)
        {
            if (_dinnerIds.Contains(dinnerId))
            {
                _dinnerIds.Remove(dinnerId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void AddMenuReview(MenuReviewId menuReviewId)
        {
            if (!_menuReviewIds.Contains(menuReviewId))
            {
                _menuReviewIds.Add(menuReviewId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void RemoveMenuReview(MenuReviewId menuReviewId)
        {
            if (_menuReviewIds.Contains(menuReviewId))
            {
                _menuReviewIds.Remove(menuReviewId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void AddSection(MenuSection section)
        {
            _sections.Add(section);
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void RemoveSection(MenuSectionId sectionId)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section != null)
            {
                _sections.Remove(section);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void UpdateSection(MenuSection section)
        {
            var existingSection = _sections.Find(s => s.Id == section.Id);
            if (existingSection != null)
            {
                _sections.Remove(existingSection);
                _sections.Add(section);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void AddMenuItemToSection(MenuSectionId sectionId, MenuItem item)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section != null)
            {
                section.AddItem(item);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void RemoveMenuItemFromSection(MenuSectionId sectionId, MenuItemId itemId)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section != null)
            {
                section.RemoveItem(itemId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void UpdateMenuItemInSection(MenuSectionId sectionId, MenuItem item)
        {
            var section = _sections.Find(s => s.Id == sectionId);
            if (section != null)
            {
                section.UpdateItem(item);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }
    }
}
