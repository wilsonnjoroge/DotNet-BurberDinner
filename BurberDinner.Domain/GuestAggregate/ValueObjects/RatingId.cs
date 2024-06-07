

using BurberDinner.Domain.Common.Models;
using System;

namespace BurberDinner.Domain.MenuReviewAggregate.ValueObjects
{
    public class RatingId : ValueObject
    {
        public Guid Value { get; }

        private RatingId(Guid value)
        {
            Value = value;
        }

        public static RatingId CreateUnique()
        {
            return new RatingId(Guid.NewGuid());
        }

        public static RatingId Create(Guid value)
        {
            return new RatingId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
