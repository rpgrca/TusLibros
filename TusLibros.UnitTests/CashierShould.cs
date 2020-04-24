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
        private const decimal ANOTHER_VALID_PRICE = 3;

        [Fact]
        public void Test0()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(null));
            Assert.Equal(Cashier.PRICELIST_IS_NULL_ERROR, exception.Message);
        }

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

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void Test4(decimal price)
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(price));
            var total = cashier.Checkout(cart);
            Assert.Equal(price, total);
        }

        [Fact]
        public void Test5()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(GetEmptyPricelist()));
            Assert.Equal(Cashier.PRICELIST_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void Test6()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 4);
            var cashier = new Cashier(GetPricelistWithTwoValidItems());
            var total = cashier.Checkout(cart);
            Assert.Equal(22, total);
        }

        [Fact]
        public void Test7()
        {
             var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart));
            Assert.Equal(Cashier.ITEM_NOT_IN_PRICELIST_ERROR, exception.Message);
        }

        private Dictionary<object, decimal> GetPricelistWithOneValidItem(decimal price)
        {
            return new Dictionary<object, decimal>
            {
                { VALID_ITEM, price }
            };
        }

        private Dictionary<object, decimal> GetPricelistWithTwoValidItems()
        {
            return new Dictionary<object, decimal>
            {
                { VALID_ITEM, VALID_PRICE },
                { ANOTHER_VALID_ITEM, ANOTHER_VALID_PRICE }
            };
        }
        private Dictionary<object, decimal> GetEmptyPricelist()
        {
            return new Dictionary<object, decimal>();
        }
    }
}