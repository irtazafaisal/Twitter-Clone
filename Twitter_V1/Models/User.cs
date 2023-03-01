namespace Twitter_V1.Models
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsAdmin { get; set; }
        public string Pic { get; set; }
    }
}
