using System.Collections.Generic;
using TusLibros.Core;

namespace TusLibros.API.UnitTests.Fakes
{
    internal class TusLibrosRestAPIStubBuilder
    {
        private IAuthenticator _authenticator;
        private IMerchantAdapter _merchantAdapter;
        private IClock _internalClock;
        private Dictionary<object, decimal> _pricelist;
        private List<object> _catalog;
        private Dictionary<string, List<object>> _balance;

        public TusLibrosRestAPIStubBuilder AuthenticatesWith(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            return this;
        }

        public TusLibrosRestAPIStubBuilder MeasuresTimeWith(IClock clock)
        {
            _internalClock = clock;
            return this;
        }

        public TusLibrosRestAPIStubBuilder CommunicatesWith(IMerchantAdapter merchantAdapter)
        {
            _merchantAdapter = merchantAdapter;
            return this;
        }

        public TusLibrosRestAPIStubBuilder UsesCatalog(List<object> catalog)
        {
            _catalog = catalog;
            return this;
        }

        public TusLibrosRestAPIStubBuilder UsesPricelist(Dictionary<object, decimal> pricelist)
        {
            _pricelist = pricelist;
            return this;
        }

        public TusLibrosRestAPIStubBuilder UsesBalance(Dictionary<string, List<object>> balance)
        {
            _balance = balance;
            return this;
        }

        internal TusLibrosRestAPI Build()
        {
            var internalClock = _internalClock ?? new ClockStubBuilder().Build();
            var authenticator = _authenticator ?? new AuthenticatorStubBuilder().Build();
            var merchantAdapter = _merchantAdapter ?? new MerchantAdapterStubBuilder().Build();
            var pricelist = _pricelist ?? new Dictionary<object, decimal>();
            var catalog = _catalog ?? new List<object>();
            var balance = _balance ?? new Dictionary<string, List<object>>();

            return new TusLibrosRestAPI(authenticator, merchantAdapter, internalClock, pricelist, catalog, balance);
        }
    }
}