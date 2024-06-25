

namespace Base.Entites
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; }=string.Empty;
    }
}
