using System.Collections.Generic;
using System;
using Xunit;
using TusLibros.API.UnitTests.Fakes;

namespace TusLibros.API.UnitTests
{
    public class TusLibrosRestAPIShould
    {
        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullAuthenticator_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(null,
                    new MerchantAdapterDummy(),
                    new ClockStubBuilder().Build(),
                    new List<object>(),
                    new List<object>()));
            Assert.Equal(TusLibrosRestAPI.AUTHENTICATOR_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullMerchantAdapter_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(new AuthenticatorStubBuilder().Build(),
                    null,
                    new ClockStubBuilder().Build(),
                    new List<object>(),
                    new List<object>()));
            Assert.Equal(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullClock_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(new AuthenticatorStubBuilder().Build(),
                    new MerchantAdapterDummy(),
                    null,
                    new List<object>(),
                    new List<object>()));
            Assert.Equal(TusLibrosRestAPI.CLOCK_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullPricelist_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(new AuthenticatorStubBuilder().Build(),
                    new MerchantAdapterDummy(),
                    new ClockStubBuilder().Build(),
                    null,
                    new List<object>()));
            Assert.Equal(Cashier.PRICELIST_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullCatalog_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(new AuthenticatorStubBuilder().Build(),
                    new MerchantAdapterDummy(),
                    new ClockStubBuilder().Build(),
                    new List<object>(),
                    null));
            Assert.Equal(Cart.CATALOG_IS_NULL_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void GivenANewTusLibrosRestAPI_WhenCreatingCartWithInvalidClientId_ThenAnExceptionIsThrown(string invalidId)
        {
            const string anyPassword = "abc";
            var sut = new TusLibrosRestAPIStubBuilder()
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => sut.CreateCart(invalidId, anyPassword));
            Assert.Equal(TusLibrosRestAPI.INVALID_CLIENTID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenANewTusLibrosRestAPI_WhenCreatingCartWithInvalidPassword_ThenAnExceptionIsThrown(string invalidPassword)
        {
            const string anyClientId = "abc";
            var sut = new TusLibrosRestAPIStubBuilder()
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => sut.CreateCart(anyClientId, invalidPassword));
            Assert.Equal(TusLibrosRestAPI.INVALID_PASSWORD_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingCartWithValidData_ThenValidCartIdIsReturned()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                       .Returns(true)
                                       .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                       .Returns(new DateTime(2020, 4, 27))
                                       .Build())
                .Build();

            var cartId = sut.CreateCart("usuarioValido", "passwordValido");
            Assert.NotEmpty(cartId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GivenANewTusLibrosRestAPI_WhenListingAnInvalidCartId_ThenAnExceptionIsThrown(string invalidCartId)
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 27))
                                        .Build())
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart(invalidCartId));
            Assert.Equal(TusLibrosRestAPI.CART_ID_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenListingAnEmptyExpiredCart_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 27))
                                        .IsExpired(true)
                                        .Build())
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");

            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart(cartId));
            Assert.Equal(TusLibrosRestAPI.CART_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenListingAnExpiredCartWithItems_ThenAnExceptionIsThrown()
        {
            // TODO
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithInvalidCartId_ThenAnExceptionIsThrown()
        {
            // TODO
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithValidCartId_ThenItemsCanBeListed()
        {
            // TODO
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithExpiredCart_ThenAnExceptionIsThrown()
        {
            // TODO
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithWrongIsbn__ThenAnExceptionIsThrown()
        {
            // TODO
        }
    }
}