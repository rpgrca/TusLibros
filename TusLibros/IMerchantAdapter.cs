namespace TusLibros
{
    public interface IMerchantAdapter
    {
        void Debit(decimal total, string creditCardNumber);
    }
}