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
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();
            Assert.NotNull(cashier);
        }

        [Fact]
        public void GivenANewCashier_WhenInitializedWithAValidPricelist_ThenTheMerchantIsNotCalled()
        {
            var merchantSpy = new MerchantSpy();
            var _ = new Cashier(GetPricelistWithOneValidItem(1), merchantSpy);
            Assert.Equal(0, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithNullCart_ThenThrowsAnException()
        {
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(null, GetValidCreditCard()));
            Assert.Equal(Cashier.CART_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithNullCart_ThenTheMerchantIsNotCalled()
        {
            var merchantSpy = new MerchantSpy();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), merchantSpy);
            Assert.Throws<ArgumentException>(() => cashier.Checkout(null, GetValidCreditCard()));
            Assert.Equal(0, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithEmptyCart_ThenThrowsAnException()
        {
            var cart = GetCartWithEmptyCatalog();
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();
            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(Cashier.CART_IS_EMPTY_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutWithEmptyCart_ThenTheMerchantIsNotCalled()
        {
            var merchantSpy = new MerchantSpy();
            var cashier = new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), merchantSpy);
            var cart = GetCartWithEmptyCatalog();
            Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(0, merchantSpy.ContactQuantity);
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
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(merchantSpy.SavedTotal, price);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithItem_ThenTheMerchantIsCalledOnce()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), merchantSpy);
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(1, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithSeveralItems_ThenCalculatesTheTotalSum()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 4);
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), merchantSpy);
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(22, merchantSpy.SavedTotal);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithSeveralItems_ThenTheMerchantIsCalledOnce()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 4);
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), merchantSpy);
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(1, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithInvalidItem_ThenThrowsAnException()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();
            var exception = Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(Cashier.ITEM_NOT_IN_PRICELIST_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithInvalidItem_ThenTheDailybookIsNotUpdated()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();
            Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Empty(cashier.GetDaybook());
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCheckingOutCartWithInvalidItem_ThenTheMerchantIsNotCalled()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(ANOTHER_VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), merchantSpy);
            Assert.Throws<KeyNotFoundException>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(0, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenAValidCashierAndCart_WhenCheckingOutWithNullCreditCardNumber_ThenThrowsAnException()
        {
            var cart = GetCartWithOneItem();
            var cashier = GetCashierWithPricelistWithOneItemAndDummyMerchant();

            var exception = Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, null));
            Assert.Equal(CreditCard.NUMBER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenAValidCashierAndCart_WhenCheckingOutWithNullCreditCardNumber_ThenTheMerchantIsNotCalled()
        {
            var merchantSpy = new MerchantSpy();
            var cart = GetCartWithOneItem();
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), merchantSpy);

            Assert.Throws<ArgumentException>(() => cashier.Checkout(cart, null));
            Assert.Equal(0, merchantSpy.ContactQuantity);
        }

        [Fact]
        public void GivenAValidPricelist_WhenCreatingCashierWithNullMerchant_ThenThrowsAnException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), null));
            Assert.Equal(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierWithPricelist_WhenCreatingANewOne_ThenDaybookShouldBeEmpty()
        {
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), new DummyMerchant());
            Assert.Empty(cashier.GetDaybook());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void GivenACashierWithPriceList_WhenCheckingOutCartWithOneItemSuccessfully_ThenAddsItToDaybook(int quantity)
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, quantity);
            var cashier = new Cashier(GetPricelistWithOneValidItem(5), new DummyMerchant());
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(quantity, cashier.GetDaybook().Count);
        }

        [Fact]
        public void GivenACashierWithPriceList_WhenCheckingOutCartWithSeveralItemsSuccessfully_ThenAddsThemToDaybook()
        {
            var cart = GetCartReadyToCheckoutWithTwoItems();
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), new DummyMerchant());
            cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(3, cashier.GetDaybook().Count);
        }

        [Fact]
        public void GivenACashierDaybook_WhenAddingItemsToDaybook_ThenTheCashierDaybookDidNotChange()
        {
            var cart = GetCartReadyToCheckoutWithTwoItems();
            var items = cart.GetItems().Count;
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), new DummyMerchant());
            cashier.Checkout(cart, GetValidCreditCard());

            var daybook = cashier.GetDaybook();
            daybook.Add(new object());
            Assert.Equal(items, cashier.GetDaybook().Count);
        }

        [Fact]
        public void GivenANewCartWithAnItem_WhenChangingTheItemInTheCopyOfItems_ThenTheCartDidNotChange()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);

            var items = cart.GetItems();
            items[0] = INVALID_ITEM;
            Assert.Single(cart.GetItems(), VALID_ITEM);
        }

        [Fact]
        public void GivenACashierCheckout_WhenMerchantReturnsOk_ThenAValidIdIsReturned()
        {
            const string expectedTransactionId = "abc";
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), new MerchantStubOk(expectedTransactionId));

            var obtainedTransactionId = cashier.Checkout(cart, GetValidCreditCard());
            Assert.Equal(expectedTransactionId, obtainedTransactionId);
        }

        [Fact]
        public void GivenACashierCheckout_WhenMerchantDiscoversStolenCard_ThenThrowsAnException()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), new MerchantStolenCardError());

            var exception = Assert.Throws<Exception>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(MerchantAdapter.CARD_IS_STOLEN_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierCheckout_WhenMerchantDiscoversStolenCard_ThenItemsAreNotIncludedInDaybook()
        {
            var cart = GetCartReadyToCheckoutWithTwoItems();
            var items = cart.GetItems().Count;
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), new MerchantStolenCardError());

            Assert.Throws<Exception>(() =>  cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Empty(cashier.GetDaybook());
        }

        [Fact]
        public void GivenACashierCheckout_WhenMerchantDiscoversNoMoneyInAccount_ThenThrowsAnException()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            var cashier = new Cashier(GetPricelistWithOneValidItem(1), new MerchantNoMoneyInAccountError());

            var exception = Assert.Throws<Exception>(() => cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Equal(MerchantAdapter.ACCOUNT_HAS_NO_MONEY_ERROR, exception.Message);
        }

        [Fact]
        public void GivenACashierCheckout_WhenMerchantDiscoversNoMoneyInAccount_ThenItemsAreNotIncludedInDaybook()
        {
            var cart = GetCartReadyToCheckoutWithTwoItems();
            var items = cart.GetItems().Count;
            var cashier = new Cashier(GetPricelistWithTwoValidItems(), new MerchantNoMoneyInAccountError());

            Assert.Throws<Exception>(() =>  cashier.Checkout(cart, GetValidCreditCard()));
            Assert.Empty(cashier.GetDaybook());
        }
    }
}