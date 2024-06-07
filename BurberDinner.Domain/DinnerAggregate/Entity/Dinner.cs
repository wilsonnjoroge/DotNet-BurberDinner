
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.Enums;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.HostAggregate.ValueObjects;
using BurberDinner.Domain.MenuAggregate.ValueObjects;
using BurberDinner.Domain.ReservationsAggregate.Entities;
using BurberDinner.Domain.ReservationsAggregate.ValueObjects;

namespace BurberDinner.Domain.DinnerAggregate.Entity
{
    public sealed class Dinner : Entity<DinnerId>
    {
        private readonly List<Reservation> _reservations = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public DateTime? StartedDateTime { get; private set; }
        public DateTime? EndedDateTime { get; private set; }
        public DinnerStatus Status { get; private set; }
        public bool IsPublic { get; private set; }
        public int MaxGuests { get; private set; }
        public Price Price { get; private set; }
        public HostId HostId { get; private set; }
        public MenuId MenuId { get; private set; }
        public string ImageUrl { get; private set; }
        public Location Location { get; private set; }
        public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Dinner(
            DinnerId id,
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            DinnerStatus status,
            bool isPublic,
            int maxGuests,
            Price price,
            HostId hostId,
            MenuId menuId,
            string imageUrl,
            Location location,
            DateTime createdDateTime,
            DateTime updatedDateTime)
            : base(id)
        {
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Status = status;
            IsPublic = isPublic;
            MaxGuests = maxGuests;
            Price = price;
            HostId = hostId;
            MenuId = menuId;
            ImageUrl = imageUrl;
            Location = location;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Dinner Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            bool isPublic,
            int maxGuests,
            Price price,
            HostId hostId,
            MenuId menuId,
            string imageUrl,
            Location location)
        {
            return new Dinner(
                DinnerId.CreateUnique(),
                name,
                description,
                startDateTime,
                endDateTime,
                DinnerStatus.Upcoming,
                isPublic,
                maxGuests,
                price,
                hostId,
                menuId,
                imageUrl,
                location,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void StartDinner()
        {
            if (Status != DinnerStatus.Upcoming)
            {
                throw new InvalidOperationException("Dinner cannot be started in its current state.");
            }

            Status = DinnerStatus.InProgress;
            StartedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void EndDinner()
        {
            if (Status != DinnerStatus.InProgress)
            {
                throw new InvalidOperationException("Dinner cannot be ended in its current state.");
            }

            Status = DinnerStatus.Ended;
            EndedDateTime = DateTime.UtcNow;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void CancelDinner()
        {
            if (Status == DinnerStatus.Ended || Status == DinnerStatus.Cancelled)
            {
                throw new InvalidOperationException("Dinner cannot be cancelled in its current state.");
            }

            Status = DinnerStatus.Cancelled;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void AddReservation(Reservation reservation)
        {
            if (_reservations.Count >= MaxGuests)
            {
                throw new InvalidOperationException("Cannot add more reservations than the maximum allowed guests.");
            }

            _reservations.Add(reservation);
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void RemoveReservation(ReservationId reservationId)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation != null)
            {
                _reservations.Remove(reservation);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }

        public void UpdateReservation(Reservation updatedReservation)
        {
            var existingReservation = _reservations.FirstOrDefault(r => r.Id == updatedReservation.Id);
            if (existingReservation != null)
            {
                _reservations.Remove(existingReservation);
                _reservations.Add(updatedReservation);
                UpdatedDateTime = DateTime.UtcNow;
            }
        }
    }
}
