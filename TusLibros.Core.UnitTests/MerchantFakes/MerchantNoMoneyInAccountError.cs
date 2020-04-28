using System;
using TusLibros.Core;

namespace TusLibros.Core.UnitTests.MerchantFakes
{
    public class MerchantNoMoneyInAccountError : IMerchantAdapter
    {
        public string Debit(decimal total, CreditCard creditCard)
        {
            throw new Exception(MerchantAdapter.ACCOUNT_HAS_NO_MONEY_ERROR);
        }
    }
}