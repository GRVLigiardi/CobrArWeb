namespace CobrArWeb.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public bool AuthenticateUser(string login, string password);

        public void CreateUser(string login, string password);
    }
}