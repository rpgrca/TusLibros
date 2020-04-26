using System;

namespace TusLibros
{
    public sealed class YearMonth
    {
        public const string CARD_HAS_EXPIRED_ERROR = "La tarjeta ya expiró.";
        public const string DATE_IS_INVALID_ERROR = "La fecha es inválida.";

        public sealed class Builder
        {
            private int _expirationYear = -1;
            private int _expirationMonth = -1;
            private int _currentYear;
            private int _currentMonth;

            public Builder()
            {
                var dateTime = DateTime.Now;
                _currentYear = dateTime.Year;
                _currentMonth = dateTime.Month;
            }

            public Builder ExpiresOn(int year, int month)
            {
                _expirationYear = year;
                _expirationMonth = month;
                return this;
            }

            public Builder ConsiderTodayAs(int year, int month)
            {
                _currentYear = year;
                _currentMonth = month;
                return this;
            }

            public YearMonth Build()
            {
                return new YearMonth(_expirationYear, _expirationMonth, _currentYear, _currentMonth);
            }
        }

        public int Year { get; }
        public int Month { get; }

        private YearMonth(int expirationYear, int expirationMonth, int currentYear, int currentMonth)
        {
            var expirationDate = CreateDateTime(expirationYear, expirationMonth);
            var currentDate = CreateDateTime(currentYear, currentMonth);

            VerifyExpirationDate(expirationDate, currentDate);

            Year = expirationYear;
            Month = expirationMonth;
        }

        private void VerifyExpirationDate(DateTime expirationDate, DateTime currentDate)
        {
            if (DateTime.Compare(expirationDate, currentDate) < 0)
            {
                throw new ArgumentException(CARD_HAS_EXPIRED_ERROR);
            }
        }

        private DateTime CreateDateTime(int year, int month)
        {
            try
            {
                return new DateTime(year, month, 1);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(DATE_IS_INVALID_ERROR, ex);
            }
        }
    }
}