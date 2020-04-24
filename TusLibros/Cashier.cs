using System;
using TusLibros.UnitTests;

namespace TusLibros
{
    public class Cashier
    {
        public Cashier()
        {
        }

        public void Checkout(Cart cart)
        {
            _ = cart ?? throw new ArgumentException();
        }
    }
}