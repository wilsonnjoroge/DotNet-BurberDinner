
using BurberDinner.Appliction.Common.Interfaces.Services;

namespace BurberDinner.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}