using System;

namespace TusLibros
{
    public class Merchant : IMerchantAdapter
    {
        public const string CARD_IS_STOLEN_ERROR = "La tarjeta de crédito fue robada.";
        public const string ACCOUNT_HAS_NO_MONEY_ERROR = "La cuenta no tiene fondos.";

        public string Debit(decimal total, CreditCard creditCard)
        {
            throw new NotImplementedException();
        }
    }
}