using CobrArWeb.Data;

namespace CobrArWeb.Models.Views.Home
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole UserRole { get; set; }
    }
}
