
using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.Bill.ValueObjects
{
    public class BillId : ValueObject
    {
        public Guid Value { get; }

        private BillId(Guid value)
        {
            Value = value;
        }

        public static BillId CreateUnique()
        {
            return new BillId(Guid.NewGuid());
        }

        public static BillId Create(Guid value)
        {
            return new BillId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
