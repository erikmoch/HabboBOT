namespace HabboBOT.Core
{
    public class Account
    {
        public string Email { get; }
        public string Password { get; }

        public Account(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}