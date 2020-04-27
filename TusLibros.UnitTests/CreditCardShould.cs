using System;
using Xunit;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class CreditCardShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData(INVALID_CREDIT_CARD_NUMBER)]
        [InlineData(ANOTHER_INVALID_CREDIT_CARD_NUMBER)]
        public void GivenAnInvalidNumber_WhenCreatingACreditCard_ThenAnExceptionIsThrown(string invalidNumber)
        {
            var builder = new CreditCard.Builder()
                .Numbered(invalidNumber)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(2020, 12)
                .CheckingOn(2020, 4);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(CreditCard.NUMBER_IS_INVALID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(INVALID_CREDIT_CARD_OWNER)]
        [InlineData(ANOTHER_INVALID_CREDIT_CARD_OWNER)]
        public void GivenAnInvalidOwner_WhenCreatingACreditCard_ThenAnExceptionIsThrown(string invalidName)
        {
            var builder = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(invalidName)
                .ExpiresOn(2020, 12)
                .CheckingOn(2020, 4);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(CreditCard.OWNER_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenAnInvalidExpirationDate_WhenCreatingACreditCard_ThenAnExceptionIsThrown()
        {
            var builder = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(-1, -1);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(YearMonth.DATE_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACurrentDateHigherThanExpirationDate_WhenCreatingACreditCard_ThenAnExceptionIsThrown()
        {
            var builder = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(1989, 12)
                .CheckingOn(1990, 1);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(CreditCard.CARD_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(VALID_YEAR + 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH + 1)]
        [InlineData(VALID_YEAR, VALID_MONTH)]
        public void GivenACurrentDateEqualOrLowerThanExpirationDate_WhenCreatingAYearMonth_ThenReturnsIt(int expirationYear, int expirationMonth)
        {
            var creditCard = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(expirationYear, expirationMonth)
                .CheckingOn(VALID_YEAR, VALID_MONTH)
                .Build();

            Assert.NotNull(creditCard);
            Assert.Equal(VALID_CREDIT_CARD_NUMBER, creditCard.Number);
            Assert.Equal(VALID_CREDIT_CARD_OWNER, creditCard.Owner);
            Assert.Equal(expirationYear, creditCard.ExpirationDate.Year);
            Assert.Equal(expirationMonth, creditCard.ExpirationDate.Month);
        }
    }
}