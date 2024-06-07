
using BurberDinner.Domain.BillAggregate.ValueObjects;
using BurberDinner.Domain.Common.Models;
using BurberDinner.Domain.DinnerAggregate.ValueObjects;
using BurberDinner.Domain.GuestAggregate.Entity;
using BurberDinner.Domain.GuestAggregate.ValueObjects;
using BurberDinner.Domain.MenuReviewAggregate.ValueObjects;
using BurberDinner.Domain.UserAggregate.ValueObjects;

namespace BurberDinner.Domain.Guest
{
    public class Guest : Entity<GuestId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImage { get; private set; }
        public decimal AverageRating { get; private set; }
        public UserId UserId { get; private set; }
        public List<DinnerId> UpcomingDinnerIds { get; private set; }
        public List<DinnerId> PastDinnerIds { get; private set; }
        public List<DinnerId> PendingDinnerIds { get; private set; }
        public List<BillId> BillIds { get; private set; }
        public List<MenuReviewId> MenuReviewIds { get; private set; }
        public List<GuestRating> Ratings { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        private Guest(
            GuestId id,
            string firstName,
            string lastName,
            string profileImage,
            decimal averageRating,
            UserId userId,
            List<DinnerId> upcomingDinnerIds,
            List<DinnerId> pastDinnerIds,
            List<DinnerId> pendingDinnerIds,
            List<BillId> billIds,
            List<MenuReviewId> menuReviewIds,
            List<GuestRating> ratings,
            DateTime createdDateTime,
            DateTime updatedDateTime)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            AverageRating = averageRating;
            UserId = userId;
            UpcomingDinnerIds = upcomingDinnerIds;
            PastDinnerIds = pastDinnerIds;
            PendingDinnerIds = pendingDinnerIds;
            BillIds = billIds;
            MenuReviewIds = menuReviewIds;
            Ratings = ratings;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static Guest Create(
            string firstName,
            string lastName,
            string profileImage,
            decimal averageRating,
            UserId userId,
            List<DinnerId> upcomingDinnerIds,
            List<DinnerId> pastDinnerIds,
            List<DinnerId> pendingDinnerIds,
            List<BillId> billIds,
            List<MenuReviewId> menuReviewIds,
            List<GuestRating> ratings)
        {
            return new Guest(
                GuestId.CreateUnique(),
                firstName,
                lastName,
                profileImage,
                averageRating,
                userId,
                upcomingDinnerIds,
                pastDinnerIds,
                pendingDinnerIds,
                billIds,
                menuReviewIds,
                ratings,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public void UpdateAverageRating()
        {if (Ratings.Count != 0)
            {
                AverageRating = (decimal)Ratings.Average(r => r.Rating);
            }
            else
            {
                // Set default value if no ratings are available
                AverageRating = 0;
            }
        }

        public void AddRating(GuestRating rating)
        {
            Ratings.Add(rating);
            UpdateAverageRating();
            UpdatedDateTime = DateTime.UtcNow;
        }
    }
}
