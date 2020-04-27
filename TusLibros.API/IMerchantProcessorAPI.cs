namespace TusLibros.API
{
    public interface IMerchantProcessorAPI
    {
        CreateCardResponse CreateCart(string clientId, string password);
    }
}