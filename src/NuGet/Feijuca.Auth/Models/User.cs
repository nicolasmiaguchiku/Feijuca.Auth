namespace Feijuca.Auth.Models
{
    public class User(Guid id, string username)
    {
        public Guid Id { get; set; } = id;
        public string Username { get; set; } = username;
    }
}
