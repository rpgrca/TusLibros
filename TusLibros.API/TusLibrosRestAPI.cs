using System;
using System.Collections.Generic;
using System.Linq;
using TusLibros.Core;

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
        public const string BALANCE_IS_NULL_ERROR = "El diario mayor es nulo.";

        private readonly Dictionary<string, Session> _carts;
        private readonly IMerchantAdapter _merchantAdapter;
        private readonly IAuthenticator _authenticator;
        private readonly IClock _internalClock;
        private readonly Dictionary<object, decimal> _pricelist;
        private readonly List<object> _catalog;
        private readonly Dictionary<string, List<object>> _balanceSheet;

        public TusLibrosRestAPI(IAuthenticator authenticator, IMerchantAdapter merchantAdapter, IClock internalClock, Dictionary<object, decimal> pricelist, List<object> catalog, Dictionary<string, List<object>> balanceSheet)
        {
            _authenticator = authenticator ?? throw new ArgumentException(AUTHENTICATOR_IS_NULL_ERROR);
            _merchantAdapter = merchantAdapter?? throw new ArgumentException(Cashier.MERCHANT_ADAPTER_IS_NULL_ERROR);
            _internalClock = internalClock ?? throw new ArgumentException(CLOCK_IS_INVALID_ERROR);
            _pricelist = pricelist ?? throw new ArgumentException(Cashier.PRICELIST_IS_NULL_ERROR);
            _catalog = catalog ?? throw new ArgumentException(Cart.CATALOG_IS_NULL_ERROR);
            _balanceSheet = balanceSheet ?? throw new ArgumentException(BALANCE_IS_NULL_ERROR);

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

            if (! _authenticator.Login(clientId, password))
            {
                throw new ArgumentException(LOGIN_IS_INVALID_ERROR);
            }

            var cartId = GenerateUniqueIdentifier();
            var session = new Session(new Cart(_catalog), _internalClock.GetDateTime(), clientId);
            _carts.Add(cartId, session);
            return cartId;
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

        public string Checkout(string cartId, string number, string expirationDate, string owner)
        {
            var session = GetValidSession(cartId);
            var creditCard = new CreditCard.Builder()
                .ExpiresOn(new YearMonth(expirationDate))
                .CheckingOn(_internalClock.GetDateTime())
                .Numbered(number)
                .OwnedBy(owner)
                .Build();
            var cashier = new Cashier(_pricelist, _merchantAdapter);
            var transactionId = cashier.Checkout(session.Cart, creditCard);

            GetBalanceSheetFor(session.ClientId)
                .AddRange(cashier.GetDaybook());

            return transactionId;
        }

        private List<object> GetBalanceSheetFor(string clientId)
        {
            if (! _balanceSheet.ContainsKey(clientId))
            {
                _balanceSheet.Add(clientId, new List<object>());
            }

            return _balanceSheet[clientId];
        }

        public List<(object, int)> ListPurchases(string clientId, string password)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException(INVALID_CLIENTID_ERROR);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(INVALID_PASSWORD_ERROR);
            }

            if (! _authenticator.Login(clientId, password))
            {
                throw new ArgumentException(LOGIN_IS_INVALID_ERROR);
            }

            if (! _balanceSheet.ContainsKey(clientId))
            {
                throw new ArgumentException(INVALID_CLIENTID_ERROR);
            }

            return _balanceSheet[clientId]
                .GroupBy(p => p)
                .Select(p => p.ToList())
                .Select(p => (p[0], p.Count))
                .ToList();
        }
    }
}