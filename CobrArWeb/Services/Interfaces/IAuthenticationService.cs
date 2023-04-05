using CobrArWeb.Data;

namespace CobrArWeb.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public User AuthenticateUser(string name, string password);

        public void CreateUser(string login, string name, string password, UserRole userRole);
    }
}