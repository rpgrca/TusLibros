using System;
using Xunit;

namespace TusLibros.UnitTests
{
    public class YearMonthShould
    {
        private const int INVALID_YEAR = -1;
        private const int INVALID_MONTH = -1;
        private const int VALID_YEAR = 2020;
        private const int VALID_MONTH = 6;

        [Theory]
        [InlineData(INVALID_YEAR, VALID_MONTH)]
        [InlineData(VALID_YEAR, INVALID_MONTH)]
        [InlineData(INVALID_YEAR, INVALID_MONTH)]
        public void GivenAnInvalidExpirationDate_WhenCreatingAYearMonth_ThenThrowsAnException(int year, int month)
        {
            var builder = new YearMonth.Builder();
            builder.ExpiresOn(year, month);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(INVALID_YEAR, VALID_MONTH)]
        [InlineData(VALID_YEAR, INVALID_MONTH)]
        [InlineData(INVALID_YEAR, INVALID_MONTH)]
        public void GivenAnInvalidCurrentDate_WhenCreatingAYearMonth_ThenThrowsAnException(int year, int month)
        {
            var builder = new YearMonth.Builder();
            builder.ConsiderTodayAs(year, month);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACurrentDateHigherThanExpirationDate_WhenCreatingAYearMonth_ThenThrowsAnException()
        {
            var builder = new YearMonth.Builder();
            builder.ConsiderTodayAs(1990, 1).ExpiresOn(1989, 12);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(YearMonth.CARD_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(VALID_YEAR + 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH + 1)]
        public void GivenACurrentDateEqualOrLowerThanExpirationDate_WhenCreatingAYearMonth_ThenReturnsIt(int expirationYear, int expirationMonth)
        {
            var yearMonth = new YearMonth.Builder()
                .ConsiderTodayAs(VALID_YEAR, VALID_MONTH)
                .ExpiresOn(expirationYear, expirationMonth)
                .Build();

            Assert.NotNull(yearMonth);
        }
    }
}