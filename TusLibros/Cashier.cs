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

        // TODO: Retornar un ticket con informacion de la venta
        public string Checkout(Cart cart, CreditCard creditCard)
        {
            _ = cart ?? throw new ArgumentException(CART_IS_NULL_ERROR);
            _ = creditCard ?? throw new ArgumentException(CreditCard.NUMBER_IS_NULL_ERROR);
            if (cart.IsEmpty())
            {
                throw new ArgumentException(CART_IS_EMPTY_ERROR);
            }

            var total = cart.GetItems().Sum(i =>
            {
                if (_priceList.ContainsKey(i))
                {
                    return _priceList[i];
                }

                throw new KeyNotFoundException(ITEM_NOT_IN_PRICELIST_ERROR);
            });

            var transactionId = Debit(total, creditCard);
            _daybook.AddRange(cart.GetItems());

            return transactionId;
        }

        private string Debit(decimal total, CreditCard creditCard) =>
            _merchantAdapter.Debit(total, creditCard);

        public List<object> GetDaybook() => new List<object>(_daybook);
    }
}