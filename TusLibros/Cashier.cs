using System;
using TusLibros.UnitTests;

namespace TusLibros
{
    public class Cashier
    {
        public const string CART_IS_NULL_ERROR = "El carrito no puede no existir";
        public const string CART_IS_EMPTY_ERROR = "El carrito no puede estar vacío";

        public Cashier()
        {
        }

        public void Checkout(Cart cart)
        {
            _ = cart ?? throw new ArgumentException(CART_IS_NULL_ERROR);
            if (cart.IsEmpty())
            {
                throw new ArgumentException(CART_IS_EMPTY_ERROR);
            }
        }
    }
}