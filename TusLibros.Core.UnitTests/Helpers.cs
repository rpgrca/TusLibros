using System;
using System.Collections.Generic;
using TusLibros.Core.UnitTests.MerchantFakes;

namespace TusLibros.Core.UnitTests
{
    public static class Helpers
    {
        public const int INVALID_YEAR = -1;
        public const int INVALID_MONTH = -1;
        public const int VALID_YEAR = 2020;
        public const int VALID_MONTH = 6;
        public static readonly object VALID_ITEM = new object();
        public static readonly object ANOTHER_VALID_ITEM = new object();
        public static readonly object INVALID_ITEM = new object();
        public const decimal VALID_PRICE = 10;
        public const decimal ANOTHER_VALID_PRICE = 3;
        public const string VALID_CREDIT_CARD_NUMBER = "1234567890123456";
        public const string INVALID_CREDIT_CARD_NUMBER = "999";
        public const string ANOTHER_INVALID_CREDIT_CARD_NUMBER = "1234-5678-123411";
        public const string SUCCESSFUL_TRANSACTION_ID = "Todo bien";
        public const string VALID_CREDIT_CARD_OWNER = "Juan Perez";
        public const string ANOTHER_VALID_CREDIT_CARD_OWNER = "Nombre con 30 letras de largo.";
        public const string YET_ANOTHER_VALID_CREDIT_CARD_OWNER = "A";
        public const string INVALID_CREDIT_CARD_OWNER = "";
        public const string ANOTHER_INVALID_CREDIT_CARD_OWNER ="Este es un nombre con más de 30 caracteres.";

        public static List<object> GetCatalogWithValidItem() => new List<object>() { VALID_ITEM };
        public static List<object> GetCatalogWithTwoValidItems() => new List<object>() { VALID_ITEM, ANOTHER_VALID_ITEM };
        public static Cart GetCartWithACatalogWithValidItem() => new Cart(GetCatalogWithValidItem());
        public static Cart GetCartWithACatalogWithTwoValidItems() => new Cart(GetCatalogWithTwoValidItems());
        public static Cart GetCartWithEmptyCatalog() => new Cart(new List<object>());
        public static Cart GetCartReadyToCheckoutWithTwoItems()
        {
            var cart = GetCartWithACatalogWithTwoValidItems();
            cart.Add(VALID_ITEM, 1);
            cart.Add(ANOTHER_VALID_ITEM, 2);
            return cart;
        }

        public static CreditCard GetValidCreditCard() =>
            new CreditCard.Builder()
                .Numbered(VALID_CREDIT_CARD_NUMBER)
                .OwnedBy(VALID_CREDIT_CARD_OWNER)
                .ExpiresOn(2020, 12)
                .CheckingOn(2020, 04)
                .Build();

        public static Cart GetCartWithOneItem()
        {
            var cart = GetCartWithACatalogWithValidItem();
            cart.Add(VALID_ITEM, 1);
            return cart;
        }

        public static Cashier GetCashierWithPricelistWithOneItemAndDummyMerchant() =>
            new Cashier(GetPricelistWithOneValidItem(VALID_PRICE), new DummyMerchant());

        public static Dictionary<object, decimal> GetPricelistWithOneValidItem(decimal price) =>
            new Dictionary<object, decimal> { { VALID_ITEM, price } };

        public static Dictionary<object, decimal> GetPricelistWithTwoValidItems() =>
            new Dictionary<object, decimal> {
                { VALID_ITEM, VALID_PRICE },
                { ANOTHER_VALID_ITEM, ANOTHER_VALID_PRICE } };

        public static Dictionary<object, decimal> GetEmptyPricelist() =>
            new Dictionary<object, decimal>();
    }
}