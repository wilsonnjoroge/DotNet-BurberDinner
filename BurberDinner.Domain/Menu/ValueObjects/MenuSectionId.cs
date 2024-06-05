
using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.Menu.ValueObjects
{
    public class MenuSectionId : ValueObject
    {
      public Guid Value { get; }

      private MenuSectionId(Guid value)
      {
        Value = value;
      }

      public static MenuSectionId CreateUnique()
      {
        return new (Guid.NewGuid());
      }

      public override IEnumerable<object> GetEqualityComponents()
      {
          yield return Value;
      }
    }
}