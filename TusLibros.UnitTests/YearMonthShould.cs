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
        public void GivenAnInvalidDate_WhenCreatingAYearMonth_ThenThrowsAnException(int year, int month)
        {
            var exception = Assert.Throws<ArgumentException>(() => new YearMonth(year, month));
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenTwoDifferentYearMonths_WhenComparingOlder_ThenItChecksCorrectly()
        {
            var olderYearMonth = new YearMonth(1999, 12);
            var newerYearMonth = new YearMonth(2020, 4);
            var comparison = olderYearMonth < newerYearMonth;
            Assert.True(comparison);
        }

        [Fact]
        public void GivenTwoDifferentYearMonths_WhenComparingNewer_ThenItChecksCorrectly()
        {
            var olderYearMonth = new YearMonth(1999, 12);
            var newerYearMonth = new YearMonth(2020, 4);
            var comparison = olderYearMonth > newerYearMonth;
            Assert.False(comparison);
        }

        // TODO: GivenTwoDifferentYearMonths_WhenComparingEqual_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingOlder_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingNewer_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingEqual_ThenReturnsTrue
    }
}