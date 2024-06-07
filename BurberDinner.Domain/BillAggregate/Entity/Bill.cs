
using BurberDinner.Domain.BillAggregate.ValueObjects;
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.GuestAggregate.ValueObjects;
using BurberDinner.Domain.HostAggregate.ValueObjects;

namespace BurberDinner.Domain.BillAggregate.Entity
{
    public sealed class Bill : Entity<BillId>
    {
        public DinnerId DinnerId { get; private set; }
        public GuestId GuestId { get; private set; }
        public HostId HostId { get; private set; }
        public Price Price { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Bill(
            BillId billId,
            DinnerId dinnerId,
            GuestId guestId,
            HostId hostId,
            Price price,
            DateTime createdDateTime,
            DateTime updatedDateTime) : base(billId)
        {
            DinnerId = dinnerId;
            GuestId = guestId;
            HostId = hostId;
            Price = price;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Bill Create(
            DinnerId dinnerId,
            GuestId guestId,
            HostId hostId,
            Price price)
        {
            return new Bill(
                BillId.CreateUnique(),
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

        public void UpdateDinnerId(DinnerId newDinnerId)
        {
            DinnerId = newDinnerId;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateGuestId(GuestId newGuestId)
        {
            GuestId = newGuestId;
            UpdatedDateTime = DateTime.UtcNow;
        }

        public void UpdateHostId(HostId newHostId)
        {
            HostId = newHostId;
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
