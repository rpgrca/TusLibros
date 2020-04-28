using System;

namespace TusLibros.API.UnitTests.Fakes
{
    public class ClockStub : IClock
    {
        private readonly DateTime _dateTimeToReturn;

        public ClockStub(DateTime dateTimeToReturn) =>
            _dateTimeToReturn = dateTimeToReturn;

        public DateTime GetDateTime() =>
            _dateTimeToReturn;
    }
}