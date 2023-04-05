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

        public User AuthenticateUser(string name, string password)
        {
            var user = _Context.Users.FirstOrDefault(item => item.Name == name);
            if (user == null)
            {
                return null;
            }
            var hashPassword = HashPassword(password, "SALT");
            if (user.HashPassword == hashPassword)
            {
                return user;
            }
            return null;
        }

        public void CreateUser(string login, string name, string password, UserRole userRole)
        {
            var user = new User
            {
                Email = login,
                Name = name,
                HashPassword = HashPassword(password, "SALT"),
                UserRole = userRole
            };

            _Context.Users.Add(user);
            _Context.SaveChanges();
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