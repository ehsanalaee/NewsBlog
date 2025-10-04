using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<Article> Articles { get; set; }
    }
}
