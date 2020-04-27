using System;

namespace TusLibros.API
{
    public class PaymentAPI
    {
        public const string INVALID_CLIENTID_ERROR = "El id del cliente es inválido";
        public const string INVALID_PASSWORD_ERROR = "El password del cliente es inválido";

        public PaymentAPI()
        {
        }

        public string CreatCart(string clientId, string password)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException(INVALID_CLIENTID_ERROR);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(INVALID_PASSWORD_ERROR);
            }

            return default;
        }
    }
}