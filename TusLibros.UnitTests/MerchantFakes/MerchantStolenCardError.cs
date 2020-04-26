using System;
namespace TusLibros.UnitTests.MerchantFakes
{
    public class MerchantStolenCardError : IMerchantAdapter
    {
        public string Debit(decimal total, string creditCardNumber)
        {
            throw new Exception(Merchant.CARD_IS_STOLEN_ERROR);
        }
    }
}