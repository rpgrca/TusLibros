namespace TusLibros.Core
{
    public interface IMerchantAdapter
    {
        string Debit(decimal total, CreditCard creditCard);
    }
}