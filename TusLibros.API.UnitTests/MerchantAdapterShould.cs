using System.Net;
using System;
using Xunit;
using TusLibros.API;

namespace TusLibros.API.UnitTests
{
    public class MerchantAdapterShould
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Test1(string invalidId)
        {
            const string anyPassword = "abc";
            var sut = new PaymentAPI();

            var exception = Assert.Throws<ArgumentException>(() => sut.CreatCart(invalidId, anyPassword));
            Assert.Equal(PaymentAPI.INVALID_CLIENTID_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Test2(string invalidPassword)
        {
            const string anyClientId = "abc";
            var sut = new PaymentAPI();

            var exception = Assert.Throws<ArgumentException>(() => sut.CreatCart(anyClientId, invalidPassword));
            Assert.Equal(PaymentAPI.INVALID_PASSWORD_ERROR, exception.Message);
        }
    }
}