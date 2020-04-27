using System;

namespace TusLibros
{
    public sealed class YearMonth
    {
        public const int DEFAULT_YEAR = -1;
        public const int DEFAULT_MONTH = -1;
        public const string DATE_IS_INVALID_ERROR = "La fecha es inválida.";

        public int Year { get; }
        public int Month { get; }

        public YearMonth(int year, int month)
        {
            try
            {
                var dateTime = new DateTime(year, month, 1);

                Year = dateTime.Year;
                Month = dateTime.Month;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(DATE_IS_INVALID_ERROR, ex);
            }
        }

        public static bool operator < (YearMonth lhs, YearMonth rhs)
        {
            return lhs.Year < rhs.Year || (lhs.Year == rhs.Year && lhs.Month < rhs.Month);
        }

        public static bool operator > (YearMonth lhs, YearMonth rhs)
        {
            return lhs.Year > rhs.Year || (lhs.Year == rhs.Year && lhs.Month > rhs.Month);
        }
/*
        private void VerifyExpirationDate(DateTime expirationDate, DateTime currentDate)
        {
            if (DateTime.Compare(expirationDate, currentDate) < 0)
            {
                throw new ArgumentException(CARD_HAS_EXPIRED_ERROR);
            }
        }*/
    }
}