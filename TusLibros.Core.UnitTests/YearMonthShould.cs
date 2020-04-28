using System;
using Xunit;
using static TusLibros.Core.UnitTests.Helpers;

namespace TusLibros.Core.UnitTests
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

        [Theory]
        [InlineData(VALID_YEAR - 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH - 1)]
        [InlineData(VALID_YEAR - 1, VALID_MONTH - 1)]
        public void GivenTwoDifferentYearMonths_WhenComparingOlderIsSmaller_ThenReturnsTrue(int year, int month)
        {
            var olderYearMonth = new YearMonth(year, month);
            var newerYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = olderYearMonth < newerYearMonth;
            Assert.True(comparison);
        }

        [Theory]
        [InlineData(VALID_YEAR - 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH - 1)]
        [InlineData(VALID_YEAR - 1, VALID_MONTH - 1)]
        public void GivenTwoDifferentYearMonths_WhenComparingOlderIsBigger_ThenReturnsFalse(int year, int month)
        {
            var olderYearMonth = new YearMonth(year, month);
            var newerYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = olderYearMonth > newerYearMonth;
            Assert.False(comparison);
        }

        [Theory]
        [InlineData(VALID_YEAR - 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH - 1)]
        [InlineData(VALID_YEAR - 1, VALID_MONTH - 1)]
        public void GivenTwoDifferentYearMonths_WhenComparingNewerIsBigger_ThenReturnsTrue(int year, int month)
        {
            var olderYearMonth = new YearMonth(year, month);
            var newerYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = newerYearMonth > olderYearMonth;
            Assert.True(comparison);
        }

        [Theory]
        [InlineData(VALID_YEAR - 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH - 1)]
        [InlineData(VALID_YEAR - 1, VALID_MONTH - 1)]
        public void GivenTwoDifferentYearMonths_WhenComparingNewerIsSmaller_ThenReturnsFalse(int year, int month)
        {
            var olderYearMonth = new YearMonth(year, month);
            var newerYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = newerYearMonth < olderYearMonth;
            Assert.False(comparison);
        }

        [Fact]
        public void GivenSameYearMonth_WhenComparingForSmaller_ThenReturnsFalse()
        {
            var firstYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var secondYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = firstYearMonth < secondYearMonth;
            Assert.False(comparison);
        }

        [Fact]
        public void GivenSameYearMonth_WhenComparingForBigger_ThenReturnsFalse()
        {
            var firstYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var secondYearMonth = new YearMonth(VALID_YEAR, VALID_MONTH);
            var comparison = firstYearMonth > secondYearMonth;
            Assert.False(comparison);
        }

        // TODO: GivenTwoDifferentYearMonths_WhenComparingEqual_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingOlder_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingNewer_ThenReturnsFalse
        // TODO: GivenTwoEqualYearMonths_WhenComparingEqual_ThenReturnsTrue

        [Fact]
        public void GivenNewYearMonth_WhenInitializedWithCorrectString_ThenBuildsIt()
        {
            var yearMonth = new YearMonth("042024");
            Assert.Equal(4, yearMonth.Month);
            Assert.Equal(2024, yearMonth.Year);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("invalidDate")]
        [InlineData("12020")]
        [InlineData("404040")]
        public void GivenNewYearMonth_WhenInitializedWithIncorrectString_ThenAnExceptionIsThrown(string invalidDate)
        {
            var exception = Assert.Throws<ArgumentException>(() => new YearMonth(invalidDate));
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }
    }
}