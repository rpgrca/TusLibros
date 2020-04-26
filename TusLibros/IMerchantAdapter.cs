namespace TusLibros
{
    public interface IMerchantAdapter
    {
        string Debit(decimal total, string creditCardNumber);
    }

    public abstract class Merchant
    {
        public const string CARD_IS_STOLEN_ERROR = "La tarjeta de crédito fue robada.";

        public const string ACCOUNT_HAS_NO_MONEY_ERROR = "La cuenta no tiene fondos.";
    }
}