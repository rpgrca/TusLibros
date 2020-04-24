using System;
using System.Reflection;
using TusLibros;
using Xunit;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class CashierShould
    {
        [Fact]
        public void Test1()
        {
            var cashier = new Cashier();
            Assert.NotNull(cashier);
        }

        [Fact]
        public void Test2()
        {
            var cashier = new Cashier();
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(null));
            Assert.Equal(Cashier.CART_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void Test3()
        {
            var cart = GetCartWithEmptyCatalog();
            var cashier = new Cashier();
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }
    }
}