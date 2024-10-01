namespace SimpleWebApi.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordHashSalt { get; set; }
    }
}
