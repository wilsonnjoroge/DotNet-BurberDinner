
using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.MenuAggregate.ValueObjects
{
    public class MenuId : ValueObject
    {
      public Guid Value { get; }

      private MenuId(Guid value)
      {
        Value = value;
      }

      public static MenuId CreateUnique()
      {
        return new (Guid.NewGuid());
      }

      public override IEnumerable<object> GetEqualityComponents()
      {
          yield return Value;
      }
    }
}