using System;

namespace TusLibros.API.UnitTests.Fakes
{
    public class ClockStub : IClock
    {
        private DateTime _dateTime;

        public ClockStub(DateTime initialDate) =>
            _dateTime = initialDate;

        public DateTime GetDateTime() =>
            _dateTime;

        public IClockCompare Has(DateTime dateTime) =>
            new Clock.ClockCompare(dateTime);

        public void AddMinutes(int minutes) =>
            _dateTime = _dateTime.AddMinutes(minutes);
    }
}