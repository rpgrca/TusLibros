namespace TusLibros.API.UnitTests.Fakes
{
    public class MerchantAdapterDummy : IMerchantAdapter
    {
        public string Debit(decimal total, CreditCard creditCard)
        {
            throw new System.NotImplementedException();
        }
    }
}