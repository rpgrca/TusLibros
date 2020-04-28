namespace TusLibros.API
{
    public interface IAuthenticator
    {
        bool Login(string clientId, string password);
    }
}