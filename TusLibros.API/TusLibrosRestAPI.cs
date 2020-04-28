using System;
using System.Collections.Generic;
using System.Linq;

namespace TusLibros.API
{
    public class TusLibrosRestAPI
    {
        public const string INVALID_CLIENTID_ERROR = "El id del cliente es inválido.";
        public const string INVALID_PASSWORD_ERROR = "El password del cliente es inválido.";
        public const string CART_ID_IS_INVALID_ERROR = "El id del carrito es inválido.";
        public const string CLOCK_IS_INVALID_ERROR = "El reloj no puede ser nulo.";
        public const string CART_HAS_EXPIRED_ERROR = "El carrito ha expirado";
        public const string AUTHENTICATOR_IS_NULL_ERROR = "El autenticador no puede ser nulo.";
        public const string LOGIN_IS_INVALID_ERROR = "La información es incorrecta.";

        private readonly Dictionary<string, Session> _carts;
        private readonly IMerchantAdapter _merchantAdapter;
        private readonly IAuthenticator _authenticator;
        private readonly IClock _internalClock;
        private readonly List<object> _pricelist;
        private readonly List<object> _catalog;

        public TusLibrosRestAPI(IAuthenticator authenticator, IMerchantAdapter merchantAdapter, IClock internalClock, List<object> pricelist, List<object> catalog)
        {
            _authenticator = authenticator ?? throw new ArgumentException(AUTHENTICATOR_IS_NULL_ERROR);
            _merchantAdapter = merchantAdapter?? throw new ArgumentException(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR);
            _internalClock = internalClock ?? throw new ArgumentException(CLOCK_IS_INVALID_ERROR);
            _pricelist = pricelist ?? throw new ArgumentException(Cashier.PRICELIST_IS_NULL_ERROR);
            _catalog = catalog ?? throw new ArgumentException(Cart.CATALOG_IS_NULL_ERROR);

            _carts = new Dictionary<string, Session>();
        }

        public string CreateCart(string clientId, string password)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException(INVALID_CLIENTID_ERROR);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(INVALID_PASSWORD_ERROR);
            }

            if (_authenticator.Login(clientId, password))
            {
                var cartId = GenerateUniqueIdentifier();
                var session = new Session(new Cart(_catalog), _internalClock.GetDateTime());
                _carts.Add(cartId, session);
                return cartId;
            }

            throw new ArgumentException(LOGIN_IS_INVALID_ERROR);
        }

        private string GenerateUniqueIdentifier() =>
            Guid.NewGuid().ToString();

        public List<(object, int)> ListCart(string cartId) =>
            GetValidSession(cartId)
                .Cart.GetItems()
                .GroupBy(p => p)
                .Select(p => p.ToList())
                .Select(p => (p[0], p.Count))
                .ToList();

        public void AddToCart(string cartId, object item, int quantity) =>
            GetValidSession(cartId).Cart
                .Add(item, quantity);

        private Session GetValidSession(string cartId)
        {
            var session = _carts
                .Where(p => p.Key == cartId)
                .Select(p => p.Value)
                .SingleOrDefault();

            if (session is null)
            {
                throw new ArgumentException(CART_ID_IS_INVALID_ERROR);
            }

            VerifySessionHasNotExpired(session);
            return session;
        }

        private void VerifySessionHasNotExpired(Session session)
        {
            var currentDateTime = _internalClock.GetDateTime();

            if (_internalClock.Has(session.LastUsed.AddMinutes(30)).ExpiredOn(currentDateTime))
            {
                throw new ArgumentException(CART_HAS_EXPIRED_ERROR);
            }

            session.LastUsed = currentDateTime;
        }
    }
}