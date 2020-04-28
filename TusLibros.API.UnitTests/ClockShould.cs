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
            var expirationDateTime = new DateTime(2020, 4, 28, 7, 0, 0);
            var currentDateTime = new DateTime(2020, 4, 28, 7, 0, 1);

            Assert.True(clock.Has(expirationDateTime).ExpiredOn(currentDateTime));
        }

        [Fact]
        public void WithNewClock_WhenAskedIfExpiresOnExpiredDate_ThenReturnsTrue()
        {
            var clock = new Clock();
            var currentDateTime = new DateTime(2020, 4, 28, 7, 0, 0);
            var expirationDateTime = new DateTime(2020, 4, 28, 7, 0, 1);

            Assert.False(clock.Has(expirationDateTime).ExpiredOn(currentDateTime));
        }

        [Fact]
        public void WithNewClock_WhenAskedIfExpiresOnSameDay_ThenReturnsFalse()
        {
            var clock = new Clock();
            var currentDateTime = new DateTime(2020, 4, 28, 7, 0, 0);
            var expirationDateTime = new DateTime(2020, 4, 28, 7, 0, 0);

            Assert.False(clock.Has(expirationDateTime).ExpiredOn(currentDateTime));
        }
    }
}