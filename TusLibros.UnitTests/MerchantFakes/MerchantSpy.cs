using TusLibros;

namespace TusLibros.MerchantFakes
{
    // Un Spy o Espía es un Fake que graba la información que se le envía.
    public class MerchantSpy : IMerchantAdapter
    {
        public const string TRANSACTION_ID = "TODO_OK";
        public decimal SavedTotal = default;
        public string SavedCreditCardNumber = default;

        public string Debit(decimal total, string creditCardNumber)
        {
            SavedTotal = total;
            SavedCreditCardNumber = creditCardNumber;
            return TRANSACTION_ID;
        }
    }
}