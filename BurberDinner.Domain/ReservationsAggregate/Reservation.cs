using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.Reservation.ValueObjects;
using BurberDinner.Domain.Dinner.ValueObjects;
using BurberDinner.Domain.Guest.ValueObjects;
using BurberDinner.Domain.Host.ValueObjects;


namespace BurberDinner.Domain.Reservation.Entities
{
    public sealed class Reservation : Entity<ReservationId>
    {
        public DinnerId DinnerId { get; private set; }
        public GuestId GuestId { get; private set; }
        public HostId HostId { get; private set; }
        public Price Price { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Reservation(
            ReservationId reservationId,
            DinnerId dinnerId,
            GuestId guestId,
            HostId hostId,
            Price price,
            DateTime createdDateTime,
            DateTime updatedDateTime) : base(reservationId)
        {
            DinnerId = dinnerId;
            GuestId = guestId;
            HostId = hostId;
            Price = price;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Reservation Create(
            DinnerId dinnerId,
            GuestId guestId,
            HostId hostId,
            Price price)
        {
            return new Reservation(
                ReservationId.CreateUnique(),
                dinnerId,
                guestId,
                hostId,
                price,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void UpdatePrice(Price newPrice)
        {
            Price = newPrice;
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
