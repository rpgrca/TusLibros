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
        }
    }
}