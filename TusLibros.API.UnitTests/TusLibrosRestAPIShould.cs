using System.Collections.Generic;
using System.Net;
using System;
using Xunit;
using TusLibros.API;
using TusLibros.API.UnitTests.Fakes;
using Moq;

namespace TusLibros.API.UnitTests
{
    public class TusLibrosRestAPIShould
    {
        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullAuthenticator_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TusLibrosRestAPI(null, new MerchantAdapterDummy(), new ClockDummy(), new List<object>(), new List<object>()));
            Assert.Equal(TusLibrosRestAPI.AUTHENTICATOR_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullMerchantAdapter_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TusLibrosRestAPI(new AuthenticatorDummy(), null, new ClockDummy(), new List<object>(), new List<object>()));
            Assert.Equal(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullClock_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TusLibrosRestAPI(new AuthenticatorDummy(), new MerchantAdapterDummy(), null, new List<object>(), new List<object>()));
            Assert.Equal(TusLibrosRestAPI.CLOCK_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullPricelist_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TusLibrosRestAPI(new AuthenticatorDummy(), new MerchantAdapterDummy(), new ClockDummy(), null, new List<object>()));
            Assert.Equal(Cashier.PRICELIST_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewTusLibrosRestAPI_WhenCreatingWithNullCatalog_ThenAnExceptionIsThrown()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TusLibrosRestAPI(new AuthenticatorDummy(), new MerchantAdapterDummy(), new ClockDummy(), new List<object>(), null));
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
/*
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Test1(string invalidCartId)
        {
            var sut = new TusLibrosRestAPI(new DummyMerchantProcessor(), new ClockDummy());
            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart(invalidCartId));
            Assert.Equal(TusLibrosRestAPI.CART_ID_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void Test2()
        {
            var sut = new TusLibrosRestAPI(new DummyMerchantProcessor(), new ClockStub(new DateTime(2020, 4, 17)));
            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart("invalidCardId"));
            Assert.Equal(TusLibrosRestAPI.CART_ID_IS_INVALID_ERROR, exception.Message);
        }

        [Fact]
        public void Test3()
        {
            const string expectedCartId = "ABCDEFG";
            var expectedResponse = $"0|{expectedCartId}";
            var clockStub = new Mock<IClock>();
            clockStub.SetupSequence(p => p.GetDateTime())
                .Returns(new DateTime(2020, 4, 27, 12, 21, 0))
                .Returns(new DateTime(2020, 4, 27, 12, 52, 0));

            var sut = new TusLibrosRestAPI(new MerchantProcessorCreateCartStub(expectedResponse), clockStub.Object);
            var cartId = sut.CreateCart("validClientId", "validPassword");
            var exception = Assert.Throws<ArgumentException>(() => sut.ListCart(cartId));
            Assert.Equal(TusLibrosRestAPI.CART_HAS_EXPIRED_ERROR, exception.Message);
        }
*/
/*
        [Fact]
        public void Test1()
        {
            var sut = new TusLibrosRestAPI(new MerchantProcessorStub());
            var cartId = sut.CreateCart("usuarioValido", "passwordValido");

            sut.AddToCart(cartId, "validIsbn", 1);
        }*/

        // TODO: No puede agregarlo
    }

/*
    public class MerchantAPIStubBuilder
    {
        private List<Action<IMerchantProcessorAPI>> _actions = new List<Action<IMerchantProcessorAPI>>();

        public MerchantAPIStubBuilder
    }*/
}