using System;

namespace TusLibros.API
{
    public interface IClockCompare
    {
        bool ExpiredOn(DateTime expirationDate);
    }

    public interface IClock
    {
        DateTime GetDateTime();
        IClockCompare Has(DateTime dateTime);
    }
}