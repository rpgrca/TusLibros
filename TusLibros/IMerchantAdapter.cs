namespace TusLibros
{
    public interface IMerchantAdapter
    {
        string Debit(decimal total, CreditCard creditCard);
    }
}