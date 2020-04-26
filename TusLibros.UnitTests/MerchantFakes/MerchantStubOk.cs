using TusLibros;

namespace TusLibros.UnitTests.MerchantFakes
{
    // Un Stub devuelve un valor fijo, normalmente se inicializa en el constructor.
    public class MerchantStubOk : IMerchantAdapter
    {
        private readonly string _transactionIdToReturn;

        public MerchantStubOk(string transactionIdToReturn) => _transactionIdToReturn = transactionIdToReturn;

        public string Debit(decimal total, string creditCardNumber) =>  _transactionIdToReturn;
    }
}