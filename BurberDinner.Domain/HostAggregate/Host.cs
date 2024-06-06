
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Host.ValueObjects;
using BurberDinner.Domain.Menu.ValueObjects;
using BurberDinner.Domain.Dinner.ValueObjects;


namespace BurberDinner.Domain.Host.Entities
{
    public sealed class Host : Entity<HostId>
    {
        private readonly List<MenuId> _menuIds = new();
        private readonly List<DinnerId> _dinnerIds = new();

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImage { get; private set; }
        public double AverageRating { get; private set; }
        public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();
        public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Host(
            HostId hostId,
            string firstName,
            string lastName,
            string profileImage,
            double averageRating,
            DateTime createdDateTime,
            DateTime updatedDateTime) : base(hostId)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            AverageRating = averageRating;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Host Create(
            string firstName,
            string lastName,
            string profileImage,
            double averageRating)
        {
            return new Host(
                HostId.CreateUnique(),
                firstName,
                lastName,
                profileImage,
                averageRating,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void AddMenu(MenuId menuId)
        {
            if (!_menuIds.Contains(menuId))
            {
                _menuIds.Add(menuId);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void RemoveMenu(MenuId menuId)
        {
            if (_menuIds.Contains(menuId))
            {
                _menuIds.Remove(menuId);
                UpdatedDateTime = DateTime.UtcNow;
            }
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

        public void UpdateProfile(string firstName, string lastName, string profileImage)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateRating(double newRating)
        {
            AverageRating = newRating;
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
