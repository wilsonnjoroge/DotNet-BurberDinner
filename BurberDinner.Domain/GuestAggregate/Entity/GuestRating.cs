
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.HostAggregate.ValueObjects;
using BurberDinner.Domain.MenuReviewAggregate.ValueObjects;

namespace BurberDinner.Domain.GuestAggregate.Entity
{
    public class GuestRating : Entity<RatingId>
    {
        public HostId HostId { get; private set; }
        public DinnerId DinnerId { get; private set; }
        public double Rating { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private GuestRating(
            RatingId id,
            HostId hostId,
            DinnerId dinnerId,
            double rating,
            DateTime createdDateTime,
            DateTime updatedDateTime)
            : base(id)
        {
            HostId = hostId;
            DinnerId = dinnerId;
            Rating = rating;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static GuestRating Create(
            HostId hostId,
            DinnerId dinnerId,
            double value)
        {
            return new GuestRating(
                RatingId.CreateUnique(),
                hostId,
                dinnerId,
                value,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }
    }
}
