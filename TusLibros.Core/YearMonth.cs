using System;

namespace TusLibros.Core
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

        public YearMonth(string date)
        {
            try
            {
                if (date.Length != 6)
                {
                    throw new ArgumentException(DATE_IS_INVALID_ERROR);
                }

                var paddedDate = date.PadLeft(6, '0');
                var dateTime = new DateTime(int.Parse(paddedDate.Substring(2, 4)), int.Parse(paddedDate.Substring(0, 2)), 1);

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
    }
}