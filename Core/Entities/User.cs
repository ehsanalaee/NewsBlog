using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Navigation
        public ICollection<Comment>? Comments { get; set; }
    }
}
