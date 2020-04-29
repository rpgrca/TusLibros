using System;
using TusLibros.Core;

namespace TusLibros.API
{
    internal class Session
    {
        public string ClientId { get; }
        public Cart Cart { get; }
        public DateTime LastUsed { get; set; }

        public Session(Cart cart, DateTime creationTime, string clientId)
        {
            Cart = cart ?? throw new ArgumentException(Cashier.CART_IS_NULL_ERROR);
            LastUsed = creationTime;
            ClientId = clientId;
        }
    }
}