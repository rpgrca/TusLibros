using System.Collections.Generic;
using System;
using TusLibros.UnitTests;

namespace TusLibros
{
    public class Cashier
    {
        public const string CART_IS_NULL_ERROR = "El carrito no puede no existir";
        public const string CART_IS_EMPTY_ERROR = "El carrito no puede estar vacío";
        public const string PRICELIST_IS_NULL_ERROR = "La lista de precios no puede no existir";
        public const string PRICELIST_IS_EMPTY_ERROR = "La lista de precios no puede estar vacía";

        private readonly Dictionary<object, decimal> _priceList;

        public Cashier(Dictionary<object, decimal> priceList)
        {
            _priceList = priceList ?? throw new ArgumentException(PRICELIST_IS_NULL_ERROR);
            if (_priceList.Count < 1)
            {
                throw new ArgumentException(PRICELIST_IS_EMPTY_ERROR);
            }
        }

        public decimal Checkout(Cart cart)
        {
            _ = cart ?? throw new ArgumentException(CART_IS_NULL_ERROR);
            if (cart.IsEmpty())
            {
                throw new ArgumentException(CART_IS_EMPTY_ERROR);
            }

            return _priceList[cart.GetBooks()[0]];
        }
    }
}