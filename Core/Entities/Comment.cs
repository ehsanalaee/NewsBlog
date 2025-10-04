using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        // Foreign keys
        [ForeignKey("ArticleId")]
        public int ArticleId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        // Navigation
        public Article Article { get; set; }
        public User User { get; set; }
    }
}
