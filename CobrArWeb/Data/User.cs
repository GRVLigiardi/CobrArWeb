namespace CobrArWeb.Data
{
    public enum UserRole
    {
        Admin,
        Employee
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string HashPassword { get; set; }
        public UserRole UserRole { get; set; }

    }
}
