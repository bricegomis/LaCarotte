﻿namespace DoList.API.Services
{
    public interface IDateTimeProvider
    {
        public DateTimeOffset GetNow();
    }
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset GetNow()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
