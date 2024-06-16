namespace LaCarotte.API.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset GetNow()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
