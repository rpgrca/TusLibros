using System.Collections.Generic;
using System;
using Xunit;
using TusLibros.Core;
using TusLibros.API.UnitTests.Fakes;

namespace TusLibros.API.UnitTests
{
    public class TusLibrosRestAPIShould
    {
        private readonly object VALID_ITEM = "1";
        private readonly object ANOTHER_VALID_ITEM = "2";
        private readonly object INVALID_ITEM = "-1";
        private readonly string VALID_CARD_NUMBER = "1234567890123456";
        private readonly string VALID_CARD_OWNER = "Juan Perez";
        private readonly string VALID_EXPIRATION_DATE = "122024";

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullAuthenticator_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new TusLibrosRestAPI(null,
                    new MerchantAdapterDummy(),
                    new ClockStubBuilder().Build(),
                    new Dictionary<object, decimal>(),
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
                    new Dictionary<object, decimal>(),
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
                    new Dictionary<object, decimal>(),
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
                    new Dictionary<object, decimal>(),
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
        public void GivenANewTusLibrosRestAPI_WhenCreatingCartWithInvalidLogin_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                       .Returns(false)
                                       .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                       .Returns(new DateTime(2020, 4, 27))
                                       .Build())
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => sut.CreateCart("usuarioInválido", "passwordInválido"));
            Assert.Equal(TusLibrosRestAPI.LOGIN_IS_INVALID_ERROR, exception.Message);
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
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithInvalidCartId_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .IsExpired(false)
                                        .Returns(new DateTime(2020, 4, 28))
                                        .Build())
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");

            var exception = Assert.Throws<ArgumentException>(() => sut.AddToCart("invalid cart id", VALID_ITEM, 1));
            Assert.Equal(TusLibrosRestAPI.CART_ID_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithValidCartId_ThenItemsCanBeListed()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 28, 12, 0, 0))
                                        .IsExpired(false)
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            sut.AddToCart(cartId, VALID_ITEM, 1);
            var itemsInCart = sut.ListCart(cartId);
            Assert.Single(itemsInCart);
            Assert.Equal(VALID_ITEM, itemsInCart[0].Item1);
            Assert.Equal(1, itemsInCart[0].Item2);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithExpiredCart_ThenAnExceptionIsThrown()
        {
            var clockStub = new ClockStub(new DateTime(2020, 4, 28, 5, 0, 0));
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(clockStub)
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            sut.AddToCart(cartId, VALID_ITEM, 1);
            clockStub.AddMinutes(31);

            var exception = Assert.Throws<ArgumentException>(() => sut.AddToCart(cartId, "1", 1));
            Assert.Equal(TusLibrosRestAPI.CART_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenAddingInvalidItems_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .IsExpired(false)
                                        .Returns(new DateTime(2020, 4, 28))
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");

            var exception = Assert.Throws<Exception>(() => sut.AddToCart(cartId, INVALID_ITEM, 1));
            Assert.Equal(Cart.NOT_IN_CATALOG_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenANewTusLibrosRestAPI_WhenAddingItemsWithWrongQuantity_ThenAnExceptionIsThrown(int invalidQuantity)
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .IsExpired(false)
                                        .Returns(new DateTime(2020, 4, 28))
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");

            var exception = Assert.Throws<ArgumentException>(() => sut.AddToCart(cartId, VALID_ITEM, invalidQuantity));
            Assert.Equal(Cart.QUANTITY_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenListingAnExpiredCartWithItems_ThenAnExceptionIsThrown()
        {
            var clockStub = new ClockStub(new DateTime(2020, 4, 28, 5, 0, 0));
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(clockStub)
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            sut.AddToCart(cartId, VALID_ITEM, 1);
            clockStub.AddMinutes(31);

            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart(cartId));
            Assert.Equal(TusLibrosRestAPI.CART_HAS_EXPIRED_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenListingCartWithSeveralItems_ThenReturnsAList()
        {
            const int quantity1 = 4;
            const int quantity2 = 13;
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 28, 12, 0, 0))
                                        .IsExpired(false)
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM, ANOTHER_VALID_ITEM })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            sut.AddToCart(cartId, VALID_ITEM, quantity1);
            sut.AddToCart(cartId, ANOTHER_VALID_ITEM, quantity2);
            var itemsInCart = sut.ListCart(cartId);
            Assert.Collection(itemsInCart,
                elem1 => { Assert.Equal(VALID_ITEM, elem1.Item1); Assert.Equal(quantity1, elem1.Item2); },
                elem2 => { Assert.Equal(ANOTHER_VALID_ITEM, elem2.Item1); Assert.Equal(quantity2, elem2.Item2); });
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCheckingOutInvalidCart_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 28, 12, 0, 0))
                                        .IsExpired(false)
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .Build();

            var exception = Assert.Throws<ArgumentException>(() => sut.Checkout("invalidCartId", VALID_CARD_NUMBER, VALID_EXPIRATION_DATE, VALID_CARD_OWNER));
            Assert.Equal(TusLibrosRestAPI.CART_ID_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCheckingOutEmptyCart_ThenAnExceptionIsThrown()
        {
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 28, 12, 0, 0))
                                        .IsExpired(false)
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .UsesPricelist(new Dictionary<object, decimal> { { VALID_ITEM, 1 } })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            var exception = Assert.Throws<ArgumentException>(() => sut.Checkout(cartId, VALID_CARD_NUMBER, VALID_EXPIRATION_DATE, VALID_CARD_OWNER));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCheckingOutCorrectCart_ThenATransactionIdIsReturned()
        {
            const string expectedTransactionId = "ABC";
            var sut = new TusLibrosRestAPIStubBuilder()
                .AuthenticatesWith(new AuthenticatorStubBuilder()
                                        .Returns(true)
                                        .Build())
                .MeasuresTimeWith(new ClockStubBuilder()
                                        .Returns(new DateTime(2020, 4, 28, 12, 0, 0))
                                        .IsExpired(false)
                                        .Build())
                .CommunicatesWith(new MerchantAdapterStubBuilder()
                                        .AlwaysReturns(expectedTransactionId)
                                        .Build())
                .UsesCatalog(new List<object> { VALID_ITEM })
                .UsesPricelist(new Dictionary<object, decimal> { { VALID_ITEM, 1 } })
                .Build();

            var cartId = sut.CreateCart("validClientId", "validPassword");
            sut.AddToCart(cartId, VALID_ITEM, 1);
            var transactionId = sut.Checkout(cartId, VALID_CARD_NUMBER, VALID_EXPIRATION_DATE, VALID_CARD_OWNER);
            Assert.Equal(expectedTransactionId, transactionId);
        }
    }
}