namespace TusLibros.API.UnitTests.Fakes
{
    public class AuthenticatorDummy : IAuthenticator
    {
        public bool Login(string clientId, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}