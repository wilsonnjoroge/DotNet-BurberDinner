

using BurberDinner.Domain.Common.Models;

namespace BurberDinner.Domain.MenuReviewAggregate.ValueObjects
{
  public sealed class MenuReviewId : ValueObject
  {
    public Guid Value { get; }

    private MenuReviewId(Guid value)
    {
      Value = value;
    }

    public static MenuReviewId CreateUnique()
    {
      return new MenuReviewId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
      yield return Value;
    }
  }
}