using System.Collections.Generic;
using System.Data.Common;
using System;
using System.Reflection;
using TusLibros;
using Xunit;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class CashierShould
    {
        private const decimal VALID_PRICE = 10;

        [Fact]
        public void Test1()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            Assert.NotNull(cashier);
        }

        [Fact]
        public void Test2()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(null));
            Assert.Equal(Cashier.CART_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void Test3()
        {
            var cart = GetCartWithEmptyCatalog();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void Test4()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var total = cashier.Checkout(cart);
            Assert.Equal(10, total);
        }

        [Fact]
        public void Test5()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(15));
            var total = cashier.Checkout(cart);
            Assert.Equal(15, total);
        }

        private Dictionary<object, decimal> GetPricelistWithOneValidItem(decimal price)
        {
            return new Dictionary<object, decimal>
            {
                { VALID_ITEM, price }
            };
        }
    }
}