using System;
using Xunit;

namespace TusLibros.API.UnitTests
{
    public class ClockShould
    {
        [Fact]
        public void WithNewClock_WhenAskedIfExpiresOnExpiredDate_ThenReturnsFalse()
        {
            var clock = new Clock();
            var lowerDateTime = new DateTime(2020, 4, 28, 7, 0, 0);
            var higherDateTime = new DateTime(2020, 4, 28, 7, 0, 1);

            Assert.False(clock.Has(lowerDateTime).ExpiredOn(higherDateTime));
        }

        [Fact]
        public void WithNewClock_WhenAskedIfExpiresOnExpiredDate_ThenReturnsTrue()
        {
            var clock = new Clock();
            var lowerDateTime = new DateTime(2020, 4, 28, 7, 0, 0);
            var higherDateTime = new DateTime(2020, 4, 28, 7, 0, 1);

            Assert.True(clock.Has(higherDateTime).ExpiredOn(lowerDateTime));
        }
    }
}