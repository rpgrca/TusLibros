using System.Net;
using System;
using Xunit;
using TusLibros.API;
using TusLibros.API.UnitTests.Fakes;

namespace TusLibros.API.UnitTests
{
    public class PaymentAPIShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void GivenANewPaymentAPI_WhenCreatingCartWithInvalidClientId_ThenAnExceptionIsThrown(string invalidId)
        {
            const string anyPassword = "abc";
            var sut = new PaymentAPI(new DummyAuthenticator());

            var exception = Assert.Throws<ArgumentException>(() => sut.CreateCart(invalidId, anyPassword));
            Assert.Equal(PaymentAPI.INVALID_CLIENTID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenANewPaymentAPI_WhenCreatingCartWithInvalidPassword_ThenAnExceptionIsThrown(string invalidPassword)
        {
            const string anyClientId = "abc";
            var sut = new PaymentAPI(new DummyAuthenticator());

            var exception = Assert.Throws<ArgumentException>(() => sut.CreateCart(anyClientId, invalidPassword));
            Assert.Equal(PaymentAPI.INVALID_PASSWORD_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewPaymentAPI_WhenCreatingCartWithValidData_ThenValidClientIdIsReturned()
        {
            const string expectedCartId = "ABCDEFG";
            var sut = new PaymentAPI(new MerchantProcessorCreateCartOkStub(expectedCartId));

            var obtainedClientId = sut.CreateCart("usuarioValido", "passwordValido");
            Assert.Equal(expectedCartId, obtainedClientId);
        }
    }
}