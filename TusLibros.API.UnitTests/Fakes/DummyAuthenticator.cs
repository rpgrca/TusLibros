using TusLibros.API;

namespace TusLibros.API.UnitTests.Fakes
{
    internal class DummyAuthenticator : IMerchantProcessorAPI
    {
        public CreateCardResponse CreateCart(string clientId, string password) =>
            throw new System.NotImplementedException();
    }
}