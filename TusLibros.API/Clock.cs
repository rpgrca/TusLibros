using System;

namespace TusLibros.API
{
    public class Clock : IClock
    {
        public class ClockCompare : IClockCompare
        {
            private readonly DateTime _expirationTime;

            public ClockCompare(DateTime expirationTime) =>
                _expirationTime = expirationTime;

            public bool ExpiredOn(DateTime currentTime) =>
                DateTime.Compare(currentTime, _expirationTime) > 0;
        }

        public const string INTERVAL_IS_INVALID_ERROR = "The minimum interval is 1 minute.";

        public DateTime GetDateTime() =>
            DateTime.Now;

        public IClockCompare Has(DateTime dateTime) =>
            new ClockCompare(dateTime);
    }
}