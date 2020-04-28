using System;

namespace TusLibros.Core
{
    public sealed class CreditCard
    {
        public const string NUMBER_IS_NULL_ERROR = "El número de la tarjeta de crédito es inválida";
        public const string NUMBER_IS_INVALID_ERROR = "El número de la tarjeta de crédito es inválida";
        public const string OWNER_IS_INVALID_ERROR = "El dueño de la tarjeta es inválido.";
        public const string CARD_HAS_EXPIRED_ERROR = "La tarjeta de crédito ha expirado.";

        public sealed class Builder
        {
            private int _expirationYear = YearMonth.DEFAULT_YEAR;
            private int _expirationMonth = YearMonth.DEFAULT_MONTH;
            private int _currentYear;
            private int _currentMonth;
            private string _number;
            private string _owner;

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

            public Builder ExpiresOn(YearMonth yearMonth)
            {
                _expirationYear = yearMonth.Year;
                _expirationMonth = yearMonth.Month;
                return this;
            }

            public Builder OwnedBy(string owner)
            {
                _owner = owner;
                return this;
            }

            public Builder Numbered(string number)
            {
                _number = number;
                return this;
            }

            public Builder CheckingOn(int year, int month)
            {
                _currentYear = year;
                _currentMonth = month;
                return this;
            }

            public Builder CheckingOn(DateTime dateTime)
            {
                _currentYear = dateTime.Year;
                _currentMonth = dateTime.Month;
                return this;
            }

            public CreditCard Build()
            {
                var expirationDate = new YearMonth(_expirationYear, _expirationMonth);
                var currentDate = new YearMonth(_currentYear, _currentMonth);
                return new CreditCard(_number, _owner, expirationDate, currentDate);
            }
        }

        public string Number { get; }
        public string Owner { get; }
        public YearMonth ExpirationDate { get; }

        private CreditCard(string number, string owner, YearMonth expirationDate, YearMonth currentDate)
        {
            ValidateNumber(number);
            ValidateOwner(owner);
            ValidateExpirationDate(expirationDate, currentDate);

            Number = number;
            Owner = owner;
            ExpirationDate = expirationDate;
        }

        private void ValidateNumber(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                throw new ArgumentException(NUMBER_IS_NULL_ERROR);
            }

            if (! decimal.TryParse(creditCardNumber, out decimal _))
            {
                throw new ArgumentException(NUMBER_IS_INVALID_ERROR);
            }

            if (creditCardNumber.Length != 16)
            {
                throw new ArgumentException(NUMBER_IS_INVALID_ERROR);
            }
        }

        private void ValidateOwner(string owner)
        {
            if (string.IsNullOrEmpty(owner) || owner.Length > 30)
            {
                throw new ArgumentException(OWNER_IS_INVALID_ERROR);
            }
        }

        private void ValidateExpirationDate(YearMonth expirationDate, YearMonth currentDate)
        {
            if (expirationDate < currentDate)
            {
                throw new ArgumentException(CARD_HAS_EXPIRED_ERROR);
            }
        }
    }
}