using TusLibros.API;

namespace TusLibros.API.UnitTests
{
    internal class MerchantProcessorCreateCartOkStub : IMerchantProcessorAPI
    {
        private readonly string _expectedCartId;

        public MerchantProcessorCreateCartOkStub(string expectedCartId) =>
            _expectedCartId = expectedCartId;

        public CreateCardResponse CreateCart(string clientId, string password) =>
            new CreateCardResponse($"0|{_expectedCartId}");
    }
}