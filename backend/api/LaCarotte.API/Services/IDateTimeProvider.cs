namespace LaCarotte.API.Services
{
    public interface IDateTimeProvider
    {
        public DateTimeOffset GetNow();
    }
}
