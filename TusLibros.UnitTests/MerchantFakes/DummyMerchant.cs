using TusLibros;

namespace TusLibros.UnitTests.MerchantFakes
{
    // Un Dummy nunca hace nada y devuelve valores por defecto
    public class DummyMerchant : IMerchantAdapter
    {
        public string Debit(decimal total, CreditCard creditCard) => default;
    }
}