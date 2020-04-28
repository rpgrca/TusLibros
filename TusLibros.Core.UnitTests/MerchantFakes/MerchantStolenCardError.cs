using System;
using TusLibros.Core;

namespace TusLibros.Core.UnitTests.MerchantFakes
{
    public class MerchantStolenCardError : IMerchantAdapter
    {
        public string Debit(decimal total, CreditCard creditCard)
        {
            throw new Exception(MerchantAdapter.CARD_IS_STOLEN_ERROR);
        }
    }
}