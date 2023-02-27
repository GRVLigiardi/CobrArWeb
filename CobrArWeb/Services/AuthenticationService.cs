using CobrArWeb.Data;
using System.Security.Cryptography;
using CobrArWeb.Services.Interfaces;

namespace CobrArWeb.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private CobrArWebContext _Context;
        public AuthenticationService(CobrArWebContext context)
        {
            _Context = context;
        }

        public bool AuthenticateUser(string login, string password)
        {
            var user = _Context.Users.FirstOrDefault(item => item.Email == login);
            if (user == null)
            {
                return false;
            }
            var hashPassword = HashPassword(password, "SALT");
            return user.HashPassword == hashPassword;
        }

        public void CreateUser(string login, string password)
        {
            var user = _Context.Users.FirstOrDefault(item => item.Email == login);
            if (user == null)
            {
                _Context.Users.Add(new User() { Email = login, HashPassword = HashPassword(password, "SALT") });
                _Context.SaveChanges();
            }
        }

        private string HashPassword(string input, string salt)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            using (var sha = SHA256.Create())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }
}