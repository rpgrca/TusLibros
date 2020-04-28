using System.Collections.Generic;

namespace TusLibros.API.UnitTests.Fakes
{
    internal class TusLibrosRestAPIStubBuilder
    {
        private IAuthenticator _authenticator;
        private IMerchantAdapter _merchantAdapter;
        private IClock _internalClock;
        private List<object> _pricelist;
        private List<object> _catalog;

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

        public TusLibrosRestAPIStubBuilder UsesPricelist(List<object> pricelist)
        {
            _pricelist = pricelist;
            return this;
        }

        internal TusLibrosRestAPI Build()
        {
            var internalClock = _internalClock ?? new ClockStubBuilder().Build();
            var authenticator = _authenticator ?? new AuthenticatorStubBuilder().Build();
            var merchantAdapter = _merchantAdapter ?? new MerchantAdapterDummy();
            var pricelist = _pricelist ?? new List<object>();
            var catalog = _catalog ?? new List<object>();

            return new TusLibrosRestAPI(authenticator, merchantAdapter, internalClock, pricelist, catalog);
        }
    }
}