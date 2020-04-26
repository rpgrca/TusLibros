using System.Linq;
using System.Collections.Generic;
using System;

namespace TusLibros
{
    public class Cashier
    {
        public const string CART_IS_NULL_ERROR = "El carrito no puede no existir";
        public const string CART_IS_EMPTY_ERROR = "El carrito no puede estar vacío";
        public const string PRICELIST_IS_NULL_ERROR = "La lista de precios no puede no existir";
        public const string PRICELIST_IS_EMPTY_ERROR = "La lista de precios no puede estar vacía";
        public const string ITEM_NOT_IN_PRICELIST_ERROR = "El producto no está en la lista de precios";
        public const string CREDIT_CARD_NUMBER_IS_NULL_ERROR = "El número de la tarjeta de crédito es inválida";
        public const string CREDIT_CARD_NUMBER_IS_INVALID_ERROR = "El número de la tarjeta de crédito es inválida";
        public const string MERCHANT_ADAPTER_IS_NULL_ERROR = "El Merchant Adapter no puede no existir";

        private readonly Dictionary<object, decimal> _priceList;
        private readonly List<object> _daybook;
        private readonly IMerchantAdapter _merchantAdapter;

        public Cashier(Dictionary<object, decimal> priceList, IMerchantAdapter merchantAdapter)
        {
            _priceList = priceList ?? throw new ArgumentException(PRICELIST_IS_NULL_ERROR);
            if (_priceList.Count < 1)
            {
                throw new ArgumentException(PRICELIST_IS_EMPTY_ERROR);
            }

            _merchantAdapter = merchantAdapter ?? throw new ArgumentException(MERCHANT_ADAPTER_IS_NULL_ERROR);
            _daybook = new List<object>();
        }

        public string Checkout(Cart cart, string creditCardNumber)
        {
            _ = cart ?? throw new ArgumentException(CART_IS_NULL_ERROR);
            if (cart.IsEmpty())
            {
                throw new ArgumentException(CART_IS_EMPTY_ERROR);
            }

            ValidateCreditCard(creditCardNumber);

            var total = cart.GetItems().Sum(i =>
            {
                if (_priceList.ContainsKey(i))
                {
                    _daybook.Add(i);
                    return _priceList[i];
                }

                throw new KeyNotFoundException(ITEM_NOT_IN_PRICELIST_ERROR);
            });

            return Debit(total, creditCardNumber);
        }

        protected virtual string Debit(decimal total, string creditCardNumber)
        {
            return _merchantAdapter.Debit(total, creditCardNumber);
        }

        private static void ValidateCreditCard(string creditCardNumber)
        {
            if (string.IsNullOrWhiteSpace(creditCardNumber))
            {
                throw new ArgumentException(CREDIT_CARD_NUMBER_IS_NULL_ERROR);
            }

            if (! decimal.TryParse(creditCardNumber, out decimal _))
            {
                throw new ArgumentException(CREDIT_CARD_NUMBER_IS_INVALID_ERROR);
            }

            if (creditCardNumber.Length != 16)
            {
                throw new ArgumentException(CREDIT_CARD_NUMBER_IS_INVALID_ERROR);
            }
        }

        public List<object> GetDaybook() => new List<object>(_daybook);
    }
}