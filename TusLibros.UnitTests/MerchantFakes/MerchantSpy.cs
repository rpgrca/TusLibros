using TusLibros;

namespace TusLibros.UnitTests.MerchantFakes
{
    // Un Spy o Espía es un Fake que graba la información que se le envía.
    public class MerchantSpy : IMerchantAdapter
    {
        public const string TRANSACTION_ID = "TODO_OK";
        public int ContactQuantity { get; private set; } = 0;
        public decimal SavedTotal;
        public CreditCard SavedCreditCard;

        public string Debit(decimal total, CreditCard creditCard)
        {
            ContactQuantity++;
            SavedTotal = total;
            SavedCreditCard = creditCard;
            return TRANSACTION_ID;
        }
    }
}