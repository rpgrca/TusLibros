using System.Collections.Generic;
using System;
using Xunit;
using static TusLibros.UnitTests.Helpers;

namespace TusLibros.UnitTests
{
    public class CashierShould
    {
        private const decimal VALID_PRICE = 10;
        private const decimal ANOTHER_VALID_PRICE = 3;
        private const string VALID_CREDIT_CARD = "1234567890123456";
        private const string INVALID_CREDIT_CARD_NUMBER = "999";

        [Fact]
        public void GivenANewCashier_WhenInitializedWithNullPricelist_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(null));
            Assert.Equal(Cashier.PRICELIST_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewCashier_WhenInitializedWithEmptyPricelist_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(GetEmptyPricelist()));
            Assert.Equal(Cashier.PRICELIST_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewCashier_WhenInitializedWithAValidPricelist_ThenItCreatesIt()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            Assert.NotNull(cashier);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithNullCart_ThenThrowsAnException()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(null, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.CART_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithEmptyCart_ThenThrowsAnException()
        {
            var cart = GetCartWithEmptyCatalog();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithItem_ThenReturnsTheTotalSum(decimal price)
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(price));
            var total = cashier.Checkout(cart, VALID_CREDIT_CARD);
            Assert.Equal(price, total);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithSeveralItems_ThenReturnsTheTotalSum()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 4);
            var cashier = new Cashier(GetPricelistWithTwoValidItems());
            var total = cashier.Checkout(cart, VALID_CREDIT_CARD);
            Assert.Equal(22, total);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithInvalidItem_ThenThrowsAnException()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
            var exception = Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.ITEM_NOT_IN_PRICELIST_ERROR, exception.Message);
        }

        [Fact]
        public void Test1()
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItem();

            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, null));
            Assert.Equal(Cashier.CREDIT_CARD_NUMBER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void Test2()
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItem();

            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, INVALID_CREDIT_CARD_NUMBER));
            Assert.Equal(Cashier.CREDIT_CARD_NUMBER_IS_INVALID_ERROR, exception.Message);
        }

        // TODO: Nombre
        // TODO: Fecha vencimiento
        // TODO: Clave

        [Fact]
        public void Test3()
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItem();

            cashier.Checkout(cart, VALID_CREDIT_CARD);
        }

        private Cart GetCartWithOneItem()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            return cart;
        }

        private Cashier GetCashierWithPricelistWithOneItem()
        {
            return new Cashier(GetPricelistWithOneValidItem(VALID_PRICE));
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