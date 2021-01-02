namespace APIOne
{
    public interface IJwtAuthenticator
    {
        public string Authenticate(string username, string password);
    }
}