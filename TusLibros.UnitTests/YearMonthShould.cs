using System;
using Xunit;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class YearMonthShould
    {
        [Theory]
        [InlineData(INVALID_YEAR, VALID_MONTH)]
        [InlineData(VALID_YEAR, INVALID_MONTH)]
        [InlineData(INVALID_YEAR, INVALID_MONTH)]
        public void GivenAnInvalidExpirationDate_WhenCreatingAYearMonth_ThenThrowsAnException(int year, int month)
        {
            var exception = Assert.Throws<ArgumentException>(() => new YearMonth(year, month));
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(INVALID_YEAR, VALID_MONTH)]
        [InlineData(VALID_YEAR, INVALID_MONTH)]
        [InlineData(INVALID_YEAR, INVALID_MONTH)]
        public void GivenAnInvalidCurrentDate_WhenCreatingAYearMonth_ThenThrowsAnException(int year, int month)
        {
            var exception = Assert.Throws<ArgumentException>(() => new YearMonth(year, month));
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }
    }
}