using TusLibros;

namespace TusLibros.MerchantFakes
{
    // Un Dummy nunca hace nada y devuelve valores por defecto
    public class DummyMerchant : IMerchantAdapter
    {
        public string Debit(decimal total, string creditCardNumber)
        {
            return default;
        }
    }
}