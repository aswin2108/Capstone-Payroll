namespace PayRollProject.Models
{
    public class AuthCred
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public int UserRole { get; set; }

    }

    public class NewAuth
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
        public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
