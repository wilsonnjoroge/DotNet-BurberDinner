using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.GuestAggregate.ValueObjects;
using BurberDinner.Domain.HostAggregate.ValueObjects;
using BurberDinner.Domain.ReservationsAggregate.ValueObjects;


namespace BurberDinner.Domain.ReservationsAggregate.Entities
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
