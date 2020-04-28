using System;

namespace TusLibros.API.UnitTests.Fakes
{
    public class ClockDummy : IClock
    {
        public DateTime GetDateTime()
        {
            throw new NotImplementedException();
        }
    }
}