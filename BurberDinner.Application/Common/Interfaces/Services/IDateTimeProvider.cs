
namespace BurberDinner.Appliction.Common.Interfaces.Services
{
  public interface IDateTimeProvider
  {
    DateTime UtcNow { get; }
  }
}