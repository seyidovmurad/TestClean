using TestClean.Application.Common.Interfaces.Services;

namespace TestClean.Infrastructure.Services;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}