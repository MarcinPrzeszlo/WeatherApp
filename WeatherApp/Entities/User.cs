namespace WeatherApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Name { get; set; }
        public int PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
