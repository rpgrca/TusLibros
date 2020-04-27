using System;

namespace TusLibros.API
{
    public class PaymentAPI
    {
        public const string INVALID_CLIENTID_ERROR = "El id del cliente es inválido.";
        public const string INVALID_PASSWORD_ERROR = "El password del cliente es inválido.";
        public const string INVALID_AUTHENTICATOR_ERROR = "El autenticador no puede ser nulo.";

        private readonly IMerchantProcessorAPI _merchantProcessor;

        public PaymentAPI(IMerchantProcessorAPI merchantProcessor)
        {
            _merchantProcessor = merchantProcessor ?? throw new ArgumentException(INVALID_AUTHENTICATOR_ERROR);
        }

        public string CreateCart(string clientId, string password)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException(INVALID_CLIENTID_ERROR);
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(INVALID_PASSWORD_ERROR);
            }

            var response = _merchantProcessor.CreateCart(clientId, password);
            if (response.Ok)
            {
                return response.Data;
            }

            throw new ArgumentException(response.Data);
        }
    }
}