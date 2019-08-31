namespace WXercises.Models
{
    public class User
    {
        public string Name { get; }
        public string Token { get; }

        public User(string name, string token)
        {
            Name = name;
            Token = token;
        }
    }
}
