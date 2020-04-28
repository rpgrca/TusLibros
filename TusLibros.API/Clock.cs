using System;

namespace TusLibros.API
{
    public class Clock : IClock
    {
        public class ClockCompare : IClockCompare
        {
            private readonly DateTime _targetDateTime;

            public ClockCompare(DateTime targetDateTime) =>
                _targetDateTime = targetDateTime;

            public bool ExpiredOn(DateTime expirationDate) =>
                DateTime.Compare(expirationDate, _targetDateTime) < 0;
        }

        public const string INTERVAL_IS_INVALID_ERROR = "The minimum interval is 1 minute.";

        public DateTime GetDateTime() =>
            DateTime.Now;

        public IClockCompare Has(DateTime dateTime) =>
            new ClockCompare(dateTime);
    }
}