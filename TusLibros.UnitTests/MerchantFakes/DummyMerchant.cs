using TusLibros;

namespace TusLibros.MerchantFakes
{
    public class DummyMerchant : IMerchantAdapter
    {
        public void Debit(decimal total, string creditCardNumber)
        {
        }
    }
}