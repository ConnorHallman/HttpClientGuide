namespace HttpClientGuide.Shared.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public User Clone()
        {
            return (User)MemberwiseClone();
        }
    }
}
