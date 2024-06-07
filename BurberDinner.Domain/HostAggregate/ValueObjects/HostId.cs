using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.HostAggregate.ValueObjects
{
    public sealed class HostId : ValueObject
    {
        public Guid Value { get; }

        private HostId(Guid value)
        {
            Value = value;
        }

        public static HostId CreateUnique()
        {
            return new HostId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}