using System;
using System.Collections.Generic;
using Xunit;
using static TusLibros.Core.UnitTests.Helpers;

namespace TusLibros.Core.UnitTests
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

        [Theory]
        [InlineData(VALID_YEAR - 1, VALID_MONTH)]
        [InlineData(VALID_YEAR, VALID_MONTH - 1)]
        public void GivenACurrentDateHigherThanExpirationDate_WhenCreatingACreditCard_ThenAnExceptionIsThrown(int expirationYear, int expirationMonth)
        {
            var builder = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(expirationYear, expirationMonth)
                .CheckingOn(VALID_YEAR, VALID_MONTH);

            var exception = Assert.Throws<ArgumentException>(() => builder.Build());
            Assert.Equal(CreditCard.CARD_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Theory]
        [MemberData(nameof(GetValidCreditCardInformation))]
        public void GivenACurrentDateEqualOrLowerThanExpirationDate_WhenCreatingAYearMonth_ThenReturnsIt(string owner, int expirationYear, int expirationMonth)
        {
            var creditCard = new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(owner)
                .ExpiresOn(expirationYear, expirationMonth)
                .CheckingOn(VALID_YEAR, VALID_MONTH)
                .Build();

            Assert.NotNull(creditCard);
            Assert.Equal(VALID_CREDIT_CARD_NUMBER, creditCard.Number);
            Assert.Equal(owner, creditCard.Owner);
            Assert.Equal(expirationYear, creditCard.ExpirationDate.Year);
            Assert.Equal(expirationMonth, creditCard.ExpirationDate.Month);
        }

        public static IEnumerable<object[]> GetValidCreditCardInformation()
        {
            foreach (var owner in new [] { VALID_CREDIT_CARD_OWNER, ANOTHER_VALID_CREDIT_CARD_OWNER, YET_ANOTHER_VALID_CREDIT_CARD_OWNER })
            {
                foreach (var expirationYear in new [] { VALID_YEAR + 1, VALID_YEAR })
                {
                    foreach (var expirationMonth in new [] { VALID_MONTH + 1, VALID_MONTH })
                    {
                        yield return new object[] { owner, expirationYear, expirationMonth };
                    }
                }
            }
        }
    }
}