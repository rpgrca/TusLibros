using System;

namespace TusLibros.UnitTests.MerchantFakes
{
    public class MerchantNoMoneyInAccountError : IMerchantAdapter
    {
        public string Debit(decimal total, CreditCard creditCard)
        {
            throw new Exception(Merchant.ACCOUNT_HAS_NO_MONEY_ERROR);
        }
    }
}