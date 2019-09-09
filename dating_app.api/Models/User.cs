namespace dating_app.api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        // properties for salting and hashing paswords
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}