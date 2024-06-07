
using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.DinnerAggregate.ValueObjects
{
    public class DinnerId : ValueObject
    {
        public Guid Value { get; }

        private DinnerId(Guid value)
        {
            Value = value;
        }

        public static DinnerId CreateUnique()
        {
            return new DinnerId(Guid.NewGuid());
        }

        public static DinnerId Create(Guid value)
        {
            return new DinnerId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
