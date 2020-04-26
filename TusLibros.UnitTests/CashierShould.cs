using System.Collections.Generic;
using System;
using Xunit;
using static TusLibros.UnitTests.Helpers;
using TusLibros.UnitTests.MerchantFakes;

namespace TusLibros.UnitTests
{
    public class CashierShould
    {
        [Fact]
        public void GivenANewCashier_WhenInitializedWithNullPricelist_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(null, new DummyMerchant()));
            Assert.Equal(Cashier.PRICELIST_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewCashier_WhenInitializedWithEmptyPricelist_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(GetEmptyPricelist(), new DummyMerchant()));
            Assert.Equal(Cashier.PRICELIST_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void GivenANewCashier_WhenInitializedWithAValidPricelist_ThenItCreatesIt()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), new DummyMerchant());
            Assert.NotNull(cashier);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithNullCart_ThenThrowsAnException()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), new DummyMerchant());
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(null, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.CART_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithEmptyCart_ThenThrowsAnException()
        {
            var cart = GetCartWithEmptyCatalog();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), new DummyMerchant());
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithItem_ThenCalculatesTheTotalSum(decimal price)
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(price), merchantSpy);
            cashier.Checkout(cart, VALID_CREDIT_CARD);
            Assert.Equal(merchantSpy.SavedTotal, price);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithSeveralItems_ThenCalculatesTheTotalSum()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 4);
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), merchantSpy);
            cashier.Checkout(cart, VALID_CREDIT_CARD);
            Assert.Equal(22, merchantSpy.SavedTotal);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithInvalidItem_ThenThrowsAnException()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), new DummyMerchant());
            var exception = Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart, VALID_CREDIT_CARD));
            Assert.Equal(Cashier.ITEM_NOT_IN_PRICELIST_ERROR, exception.Message);
        }

        [Fact]
        public void GivenAValidCashierAndCart_WhenCheckingOutWithNullCreditCardNumber_ThenThrowsAnException()
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();

            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, null));
            Assert.Equal(Cashier.CREDIT_CARD_NUMBER_IS_NULL_ERROR, exception.Message);
        }

        [Theory]
        [InlineData(INVALID_CREDIT_CARD_NUMBER)]
        [InlineData(ANOTHER_INVALID_CREDIT_CARD_NUMBER)]
        public void GivenAValidCashierAndCart_WhenCheckingOutWithInvalidCreditCardNumber_ThenThrowsAnException(string invalidCard)
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();

            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, invalidCard));
            Assert.Equal(Cashier.CREDIT_CARD_NUMBER_IS_INVALID_ERROR, exception.Message);
        }

        // TODO: Nombre
        // TODO: Fecha vencimiento
        // TODO: Clave
        // TODO: Modelar clase tarjeta de crédito
        // TODO: Unir catalogo y lista de precios
        // TODO: Book sale

        [Fact]
        public void GivenAValidPricelist_WhenCreatingCashierWithNullMerchant_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), null));
            Assert.Equal(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenAValidCashierAndCart_WhenCheckingOutWithValidCreditCard_ThenShouldReturnTransactionId()
        {
            var merchantStub = new MerchantStubOk(SUCCESSFUL_TRANSACTION_ID);
            var cart = GetCartWithOneItem();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), merchantStub);

            var transactionId = cashier.Checkout(cart, VALID_CREDIT_CARD);
            Assert.Equal(SUCCESSFUL_TRANSACTION_ID, transactionId);
        }
    }
}